using Microsoft.Data.SqlClient;
using ModelClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MusclePlanDataAccess
    {
        public static void CreateMuscle(MusclePlanModel model)
        {
            using (SqlConnection con = DBhelper.GetConnection())
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("CreateMusclee", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PlanID", model.PlanID);
                    cmd.Parameters.AddWithValue("@MuscleID", model.MuscleID);
                    cmd.Parameters.AddWithValue("@MuscleName", model.MuscleName);
                    cmd.Parameters.AddWithValue("@MuscleImageReference", model.MuscleImageReference);
                    cmd.Parameters.AddWithValue("@Description", model.Description);
                    cmd.Parameters.AddWithValue("@Day", model.Day);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteMuscle(int muscleId)
        {
            using (SqlConnection con = DBhelper.GetConnection())
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("DeleteMuscle", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MuscleID", muscleId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateMuscle(MusclePlanModel model)
        {
            using (SqlConnection con = DBhelper.GetConnection())
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("UpdateMuscle", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MuscleID", model.MuscleID);
                    cmd.Parameters.AddWithValue("@MuscleName", model.MuscleName);
                    cmd.Parameters.AddWithValue("@MuscleImageReference", model.MuscleImageReference);
                    cmd.Parameters.AddWithValue("@Description", model.Description);
                    cmd.Parameters.AddWithValue("@Day", model.Day);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<MusclePlanModel> GetMuscles()
        {
            List<MusclePlanModel> muscles = new List<MusclePlanModel>();

            try
            {
                using (SqlConnection con = DBhelper.GetConnection())
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("GetMuscle", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MusclePlanModel muscle = new MusclePlanModel
                            {
                                MuscleID = reader.GetInt32(reader.GetOrdinal("MuscleID")),
                                PlanID = reader.GetInt32(reader.GetOrdinal("PlanID")),
                                MuscleName = reader.GetString(reader.GetOrdinal("MuscleName")),
                                MuscleImageReference = reader.GetString(reader.GetOrdinal("MuscleImageReference")),
                                Description = reader.GetString(reader.GetOrdinal("Description")),
                                Day = reader.GetString(reader.GetOrdinal("Day"))
                            };

                            muscles.Add(muscle);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (log, display an error message, etc.)
                Console.WriteLine($"Error retrieving muscle information: {ex.Message}");
            }

            return muscles;
        }




        public static List<MusclePlanModel> GetMusclesByName(string muscleName = null)
        {
            List<MusclePlanModel> muscles = new List<MusclePlanModel>();

            try
            {
                using (SqlConnection con = DBhelper.GetConnection())
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("GetMusclesByName", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (!string.IsNullOrEmpty(muscleName))
                    {
                        cmd.Parameters.AddWithValue("@MuscleName", muscleName);
                    }

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MusclePlanModel muscle = new MusclePlanModel
                            {
                                MuscleID = reader.GetInt32(reader.GetOrdinal("MuscleID")),
                                PlanID = reader.GetInt32(reader.GetOrdinal("PlanID")),
                                MuscleName = reader.GetString(reader.GetOrdinal("MuscleName")),
                                MuscleImageReference = reader.GetString(reader.GetOrdinal("MuscleImageReference")),
                                Description = reader.GetString(reader.GetOrdinal("Description")),
                                Day = reader.GetString(reader.GetOrdinal("Day"))
                            };

                            muscles.Add(muscle);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (log, display an error message, etc.)
                Console.WriteLine($"Error retrieving muscle information: {ex.Message}");
            }

            return muscles;
        }


        public static MusclePlanModel GetMuscleById(int muscleId)
        {
            MusclePlanModel muscle = null;

            try
            {
                using (SqlConnection con = DBhelper.GetConnection())
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("GetMuscleByID", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@MuscleID", muscleId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            muscle = new MusclePlanModel
                            {
                                MuscleID = reader.GetInt32(reader.GetOrdinal("MuscleID")),
                                PlanID = reader.GetInt32(reader.GetOrdinal("PlanID")),
                                MuscleName = reader.GetString(reader.GetOrdinal("MuscleName")),
                                MuscleImageReference = reader.GetString(reader.GetOrdinal("MuscleImageReference")),
                                Description = reader.GetString(reader.GetOrdinal("Description")),
                                Day = reader.GetString(reader.GetOrdinal("Day"))
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (log, display an error message, etc.)
                Console.WriteLine($"Error retrieving muscle information: {ex.Message}");
            }

            return muscle;
        }

    }
}
