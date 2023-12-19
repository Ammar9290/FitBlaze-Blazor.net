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
    public class WorkoutPlanDataAccess
    {
        public static void CreateWorkoutPlan(WorkoutPlanModel model)
        {
            using (SqlConnection con = DBhelper.GetConnection())
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("CreateWorkoutPlans", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PlanName", model.PlanName);
                    cmd.Parameters.AddWithValue("@PlanImageReference", model.PlanImageReference);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteWorkoutPlan(int planId)
        {
            using (SqlConnection con = DBhelper.GetConnection())
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("DeleteWorkoutPlans", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PlanID", planId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateWorkoutPlan(WorkoutPlanModel model)
        {
            using (SqlConnection con = DBhelper.GetConnection())
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("UpdateWorkoutPlans", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PlanID", model.PlanID);
                    cmd.Parameters.AddWithValue("@PlanName", model.PlanName);
                    cmd.Parameters.AddWithValue("@PlanImageReference", model.PlanImageReference);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<WorkoutPlanModel> GetWorkoutPlans()
        {
            List<WorkoutPlanModel> plans = new List<WorkoutPlanModel>();

            try
            {
                using (SqlConnection con = DBhelper.GetConnection())
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("GetWorkoutplans", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            WorkoutPlanModel plan = new WorkoutPlanModel
                            {
                                PlanID = reader.GetInt32(reader.GetOrdinal("PlanID")),
                                PlanName = reader.GetString(reader.GetOrdinal("PlanName")),
                                PlanImageReference = reader.GetString(reader.GetOrdinal("PlanImageReference"))
                            };

                            plans.Add(plan);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (log, display an error message, etc.)
                Console.WriteLine($"Error retrieving workout plans: {ex.Message}");
            }

            return plans;
        }

        public static WorkoutPlanModel GetWorkoutById(int planId)
        {
            WorkoutPlanModel workout = null;

            try
            {
                using (SqlConnection con = DBhelper.GetConnection())
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("GetWorkoutByID", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@PlanID", planId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            workout = new WorkoutPlanModel
                            {
                                PlanID = reader.GetInt32(reader.GetOrdinal("PlanID")),
                                PlanName = reader.GetString(reader.GetOrdinal("PlanName")),
                                PlanImageReference = reader.GetString(reader.GetOrdinal("PlanImageReference"))
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (log, display an error message, etc.)
                Console.WriteLine($"Error retrieving workout information: {ex.Message}");
            }

            return workout;
        }

    }
}
