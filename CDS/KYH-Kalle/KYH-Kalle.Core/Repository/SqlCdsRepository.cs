using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using System.Text;
using HiQ.NetStandard.Util.Data;
using KYH_Kalle.Core.Entity;

namespace KYH_Kalle.Core.Repository
{
    public class SqlCdsRepository : ADbRepositoryBase, ICdsRepository
    {
        public int CreateVehicleList(int count)
        {

            const string PLATE_CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const int VIN_START = 10000000;
            const int VIN_END = 99999999;
            var colors = new List<string>(5) { "Black", "White", "Red", "Blue", "Gray" };
            var rnd = new Random();

            DbAccess.ExecuteNonQuery("dbo.sCdsClearVehiclesAndReleations", CommandType.StoredProcedure);
            SqlParameters parameters;

            for (var i = 0; i < count; i++)
            {
                var isUnique = false;
                var vin = string.Empty;
                var regNo = string.Empty;

                while (!isUnique)
                {
                    vin = rnd.Next(VIN_START, VIN_END).ToString();
                    regNo = string.Empty;
                    for (var r = 0; r < 3; r++)
                    {
                        var ci = rnd.Next(0, PLATE_CHARS.Length - 1);
                        regNo = $"{regNo}{PLATE_CHARS.Substring(ci, 1)}";



                    }

                    var n = rnd.Next(0, 999);
                    regNo = $"{regNo}{n.ToString().PadLeft(3, '0')}";

                    isUnique = IsUniqueVinAndRegNo(vin, regNo);

                }

                var c = colors[rnd.Next(0, colors.Count - 1)];
                parameters = new SqlParameters();
                parameters.AddVarChar("@VIN", 10, vin);
                parameters.AddVarChar("@RegNo", 10, regNo);
                parameters.AddNVarChar("@Manufacturer", 50, "PålStjärna");
                parameters.AddNVarChar("@Model", 50, "CCX-100");
                parameters.AddNVarChar("@Color", 50, c);
                DbAccess.ExecuteNonQuery("dbo.sCdsVehicle_Update", ref parameters, CommandType.StoredProcedure);
            }

            return count;



        }

        public IList<CustomerFull> GetCustomerList(string filter, bool wildCard)
        {
            var result = new List<CustomerFull>();
            var currentId = Guid.Empty;
            CustomerFull customer = null;

            var parameters = new SqlParameters();
            parameters.AddNVarChar("@Filter", 50, filter);
            parameters.AddBoolean("@WildCard", wildCard);

            var dr = DbAccess.ExecuteReader("[dbo].[sCdsCustomer_List]", ref parameters, CommandType.StoredProcedure);
            
            while (dr.Read())
            {
                var id = dr.GetGuid(0);
                if (id != currentId)
                {
                    if (customer != null)
                    {
                        result.Add(customer);
                    }

                    currentId = id;
                    customer = new CustomerFull()
                    {
                        Id = id, FirstName = dr.GetString(1), LastName = dr.GetString(2), City = dr.GetString(3),
                        PhoneNo = !dr.IsDBNull(4) ? dr.GetString(4) : string.Empty
                    };
                }

                if (!dr.IsDBNull(5) && customer != null)
                {
                    customer.Vehicles.Add(new VehicleSmallWithOwner()
                    {
                        Vin = dr.GetString(5), RegNo = dr.GetString(6), Manufacturer = dr.GetString(7),
                        Model = dr.GetString(8), Color = dr.GetString(9),
                        OwnerStatus = (Enums.OwnerStatus)dr.GetByte(10)
                    });
                }

            }

            if (customer != null)
            {
                result.Add(customer);
            }

            DbAccess.DisposeReader(ref dr);
            return result;
        }

        public bool TryGetVehicleByVin(string vin, out Vehicle vehicle)
        {
            vehicle = null;
            var result = false;

            var parameters = new SqlParameters();
            parameters.AddVarChar("@VIN", 10, vin);
            parameters.AddBoolean("@Result", false, ParameterDirection.Output);
            parameters.AddVarChar("@RegNo", 10, string.Empty, ParameterDirection.Output);
            parameters.AddNVarChar("@Manufacturer", 50, string.Empty, ParameterDirection.Output);
            parameters.AddNVarChar("@Model", 50, string.Empty, ParameterDirection.Output);
            parameters.AddNVarChar("@Color", 50, string.Empty, ParameterDirection.Output);
            parameters.AddUniqueIdentifier("@CustomerId", Guid.Empty, ParameterDirection.Output);
            parameters.AddNVarChar("@FirstName", 128, string.Empty, ParameterDirection.Output);
            parameters.AddNVarChar("@LastName", 128, string.Empty, ParameterDirection.Output);
            parameters.AddNVarChar("@City", 50, string.Empty, ParameterDirection.Output);
            parameters.AddNVarChar("@PhoneNo", 20, string.Empty, ParameterDirection.Output);
            parameters.AddTinyInt("@OwnerStatus", 0, ParameterDirection.Output);

            DbAccess.ExecuteNonQuery("[dbo].[sCdsVehicle_TryGetByVIN]", ref parameters, CommandType.StoredProcedure);

            result = parameters.GetBool("@Result");

            if (result)
            {
                vehicle = new Vehicle()
                {
                    Vin = vin, RegNo = parameters.GetString("@RegNo"),
                    Manufacturer = parameters.GetString("@Manufacturer"), Model = parameters.GetString("@Model"),
                    Color = parameters.GetString("@Color")
                };

                if (!parameters.IsNull("@CustomerId"))
                {
                    vehicle.Owner = new CustomerOwner()
                    {
                        Id = parameters.GetGuid("@CustomerId"),
                        FirstName = parameters.GetString("@FirstName"),
                        LastName = parameters.GetString("@LastName"),
                        City = parameters.GetString("@City"),
                        PhoneNo = parameters.GetString("@PhoneNo"),
                        OwnerStatus = (Enums.OwnerStatus)parameters.GetByte("@OwnerStatus")

                    };
                }

            }

            return result;

        }

        public bool TryGetVehicleByVinAndRegNo(string vin, string regNo, out Vehicle vehicle)
        {
            vehicle = null;
            var result = false;
            var parameters = new SqlParameters();
            parameters.AddVarChar("@VIN", 10, vin);
            parameters.AddVarChar("@RegNo", 10, regNo);
            parameters.AddBoolean("@Result", false, ParameterDirection.Output);
            parameters.AddNVarChar("@Manufacturer", 50, string.Empty, ParameterDirection.Output);
            parameters.AddNVarChar("@Model", 50, string.Empty, ParameterDirection.Output);
            parameters.AddNVarChar("@Color", 50, string.Empty, ParameterDirection.Output);
            parameters.AddUniqueIdentifier("@CustomerId", Guid.Empty, ParameterDirection.Output);
            parameters.AddNVarChar("@FirstName", 128, string.Empty, ParameterDirection.Output);
            parameters.AddNVarChar("@LastName", 128, string.Empty, ParameterDirection.Output);
            parameters.AddNVarChar("@City", 50, string.Empty, ParameterDirection.Output);
            parameters.AddNVarChar("@PhoneNo", 20, string.Empty, ParameterDirection.Output);
            parameters.AddTinyInt("@OwnerStatus", 0, ParameterDirection.Output);

            DbAccess.ExecuteNonQuery("[dbo].[sCdsVehicle_TryGetByVINAndRegNo]", ref parameters, CommandType.StoredProcedure);

            result = parameters.GetBool("@Result");

            if (result)
            {
                vehicle = new Vehicle()
                {
                    Vin = vin,
                    RegNo = regNo.ToUpper(),
                    Manufacturer = parameters.GetString("@Manufacturer"),
                    Model = parameters.GetString("@Model"),
                    Color = parameters.GetString("@Color")
                };

                if (!parameters.IsNull("@CustomerId"))
                {
                    vehicle.Owner = new CustomerOwner()
                    {
                        Id = parameters.GetGuid("@CustomerId"),
                        FirstName = parameters.GetString("@FirstName"),
                        LastName = parameters.GetString("@LastName"),
                        City = parameters.GetString("@City"),
                        PhoneNo = parameters.GetString("@PhoneNo"),
                        OwnerStatus = (Enums.OwnerStatus)parameters.GetByte("@OwnerStatus")

                    };
                }

            }

            return result;
        }

        public bool TryGetVehicleByVinRegNoAndCustomerId(string vin, string regNo, Guid customerId, out Vehicle vehicle)
        {
            vehicle = null;
            var result = false;
            var parameters = new SqlParameters();
            parameters.AddVarChar("@VIN", 10, vin);
            parameters.AddVarChar("@RegNo", 10, regNo);
            parameters.AddUniqueIdentifier("@CustomerId", customerId);
            parameters.AddBoolean("@Result", false, ParameterDirection.Output);
            parameters.AddNVarChar("@Manufacturer", 50, string.Empty, ParameterDirection.Output);
            parameters.AddNVarChar("@Model", 50, string.Empty, ParameterDirection.Output);
            parameters.AddNVarChar("@Color", 50, string.Empty, ParameterDirection.Output);
            
            parameters.AddNVarChar("@FirstName", 128, string.Empty, ParameterDirection.Output);
            parameters.AddNVarChar("@LastName", 128, string.Empty, ParameterDirection.Output);
            parameters.AddNVarChar("@City", 50, string.Empty, ParameterDirection.Output);
            parameters.AddNVarChar("@PhoneNo", 20, string.Empty, ParameterDirection.Output);
            parameters.AddTinyInt("@OwnerStatus", 0, ParameterDirection.Output);

            DbAccess.ExecuteNonQuery("[dbo].[sCdsVehicle_TryGetByVINAndRegNoAndCustomerId]", ref parameters, CommandType.StoredProcedure);

            result = parameters.GetBool("@Result");

            if (result)
            {
                vehicle = new Vehicle()
                {
                    Vin = vin,
                    RegNo = regNo.ToUpper(),
                    Manufacturer = parameters.GetString("@Manufacturer"),
                    Model = parameters.GetString("@Model"),
                    Color = parameters.GetString("@Color")
                };


                vehicle.Owner = new CustomerOwner()
                {
                    Id = customerId,
                    FirstName = parameters.GetString("@FirstName"),
                    LastName = parameters.GetString("@LastName"),
                    City = parameters.GetString("@City"),
                    PhoneNo = parameters.GetString("@PhoneNo"),
                    OwnerStatus = (Enums.OwnerStatus)parameters.GetByte("@OwnerStatus")

                };


            }

            return result;
        }

        public IList<Vehicle> GetVehicleList(string regNo)
        {
            var result = new List<Vehicle>();
            var parameters = new SqlParameters();
            parameters.AddVarChar("@RegNo", 10, regNo.Trim());
            var dr = DbAccess.ExecuteReader("[dbo].[sCdsVehicle_GetList]", ref parameters, CommandType.StoredProcedure);
            while (dr.Read())
            {
                var v = new Vehicle()
                {
                    Vin = dr.GetString(0), RegNo = dr.GetString(1), Manufacturer = dr.GetString(2),
                    Model = dr.GetString(3), Color = dr.GetString(4)
                };
                if (!dr.IsDBNull(5))
                {
                    v.Owner = new CustomerOwner()
                    {
                        Id = dr.GetGuid(5),
                        FirstName = dr.GetString(6),
                        LastName = dr.GetString(7),
                        City = dr.GetString(8),
                        PhoneNo = dr.GetString(9), 
                        OwnerStatus = Enums.OwnerStatus.Owning
                    };
                    
                }
                result.Add(v);
            }

            DbAccess.DisposeReader(ref dr);
            return result;
        }

        public bool UpdateOwner(string vin, Guid customerId, Enums.OwnerStatus status, out Vehicle vehicle)
        {
            if (TryGetVehicleByVin(vin, out vehicle))
            {
                var parameters = new SqlParameters();
                parameters.AddVarChar("@VIN", 10, vin);
                parameters.AddUniqueIdentifier("@CustomerId", customerId);
                parameters.AddTinyInt("@Status", (byte)status);
                DbAccess.ExecuteNonQuery("[dbo].[sCdsCustomerVehicle_Update]", ref parameters,
                    CommandType.StoredProcedure);

                return TryGetVehicleByVin(vin, out vehicle);

            }

            return false;
        }

        public CustomerFull GetCustomer(Guid customerId)
        {
            CustomerFull customer = null;



            var parameters = new SqlParameters();
            parameters.AddUniqueIdentifier("@CustomerId", customerId);

            var dr = DbAccess.ExecuteReader("dbo.sCdsCustomer_GetFull", ref parameters, CommandType.StoredProcedure);
            while (dr.Read())
            {
                if (customer == null)
                {
                    customer = new CustomerFull()
                    {
                        Id = customerId, 
                        FirstName = dr.GetString(1), 
                        LastName = dr.GetString(2), 
                        City = dr.GetString(3), 
                        PhoneNo = dr.GetString(4), 
                        User = new ApiUser()
                        {
                            Id = dr.GetGuid(5), 
                            DisplayName = dr.GetString(7), 
                            CustomerId = customerId
                        }

                    };
                }

                if (!dr.IsDBNull(8))
                {
                    customer.Vehicles.Add(new VehicleSmallWithOwner()
                    {
                        Vin = dr.GetString(8), 
                        RegNo = dr.GetString(9),
                        Manufacturer = dr.GetString(10), 
                        Model = dr.GetString(11), 
                        Color = dr.GetString(12), 
                        RegistrationDate = dr.GetDateTime(13),
                        OwnerStatus = (Enums.OwnerStatus)dr.GetByte(14)
                    });
                }
            }

            DbAccess.DisposeReader(ref dr);

            return customer;
        }

        private bool IsUniqueVinAndRegNo(string vin, string regNo)
        {
            var parameters = new SqlParameters();
            parameters.AddVarChar("@VIN", 10, vin);
            parameters.AddVarChar("@RegNo", 10, regNo);
            parameters.AddBoolean("@VINResult", true, ParameterDirection.Output);
            parameters.AddBoolean("@RegNoResult", true, ParameterDirection.Output);

            DbAccess.ExecuteNonQuery("dbo.sCdsIsVinAndRegUnique", ref parameters, CommandType.StoredProcedure);

            return parameters.GetBool("@VINResult") && parameters.GetBool("@RegNoResult");

        }
    }
}
