using Microsoft.Data.SqlClient;
using ModelClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ExercisePlanDataAccess
    {

        public static void CreateExercise(ExercisePlanModel model)
        {
            using (SqlConnection con = DBhelper.GetConnection())
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("CreateExercise", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MuscleID", model.MuscleID);
                    cmd.Parameters.AddWithValue("@ExericeName", model.ExerciseName);
                    cmd.Parameters.AddWithValue("@ExericeImageReference", model.ExerciseImageReference);
                    cmd.Parameters.AddWithValue("@Description", model.Description);
                    cmd.Parameters.AddWithValue("@Time", model.Time);
                    cmd.Parameters.AddWithValue("@Raps", model.Raps);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteExercise(int exerciseId)
        {
            using (SqlConnection con = DBhelper.GetConnection())
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("DeleteExercise", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ExericeID", exerciseId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateExercise(ExercisePlanModel model)
        {
            using (SqlConnection con = DBhelper.GetConnection())
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("UpdateExercise", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ExerciseID", model.ExerciseID);
                    cmd.Parameters.AddWithValue("@MuscleID", model.MuscleID);
                    cmd.Parameters.AddWithValue("@ExerciseName", model.ExerciseName);
                    cmd.Parameters.AddWithValue("@ExerciseImageReference", model.ExerciseImageReference);
                    cmd.Parameters.AddWithValue("@Description", model.Description);
                    cmd.Parameters.AddWithValue("@Time", model.Time);
                    cmd.Parameters.AddWithValue("@Raps", model.Raps);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<ExercisePlanModel> GetExercises()
        {
            List<ExercisePlanModel> exercises = new List<ExercisePlanModel>();

            try
            {
                using (SqlConnection con = DBhelper.GetConnection())
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("GetExercise", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable schemaTable = reader.GetSchemaTable();

                        // Inspect schemaTable to see the column names

                        while (reader.Read())
                        {
                            ExercisePlanModel exercise = new ExercisePlanModel
                            {
                                ExerciseID = reader.GetInt32(reader.GetOrdinal("ExericeID")),
                                MuscleID = reader.GetInt32(reader.GetOrdinal("MuscleID")),
                                ExerciseName = reader.GetString(reader.GetOrdinal("ExericeName")),
                                ExerciseImageReference = reader.GetString(reader.GetOrdinal("ExericeImageReference")),
                                Description = reader.GetString(reader.GetOrdinal("Description")),
                                Time = reader.GetInt32(reader.GetOrdinal("Time")),
                                Raps = reader.GetInt32(reader.GetOrdinal("Raps"))
                            };

                            exercises.Add(exercise);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                // Log or print the exception
                Console.WriteLine($"Error retrieving exercise information: {ex.Message}");
                throw; // Rethrow the exception to capture it in the calling code
            }

            return exercises;
        }

        public static ExercisePlanModel GetExerciseById(int exerciseId)
        {
            ExercisePlanModel exercise = null;

            try
            {
                using (SqlConnection con = DBhelper.GetConnection())
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("GetExerciseByID", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ExerciseID", exerciseId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            exercise = new ExercisePlanModel
                            {
                                ExerciseID = reader.GetInt32(reader.GetOrdinal("ExericeID")),
                                MuscleID = reader.GetInt32(reader.GetOrdinal("MuscleID")),
                                ExerciseName = reader.GetString(reader.GetOrdinal("ExericeName")),
                                ExerciseImageReference = reader.GetString(reader.GetOrdinal("ExerciseImageReference")),
                                Description = reader.GetString(reader.GetOrdinal("Description")),
                                Time = reader.GetInt32(reader.GetOrdinal("Time")),
                                Raps = reader.GetInt32(reader.GetOrdinal("Raps"))
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (log, display an error message, etc.)
                Console.WriteLine($"Error retrieving exercise information: {ex.Message}");
            }

            return exercise;
        }





        public static List<ExercisePlanModel> GetExercisesByMuscleID(int muscleId)
        {
            List<ExercisePlanModel> exercises = new List<ExercisePlanModel>();

            try
            {
                using (SqlConnection con = DBhelper.GetConnection())
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("GetExercisesByMuscleID", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@MuscleID", muscleId);

                    using(SqlDataReader reader = cmd.ExecuteReader())
{
                        while (reader.Read())
                        {
                            // Log each row retrieved from the database
                            Console.WriteLine($"ExerciseID: {reader.GetInt32(reader.GetOrdinal("ExerciseID"))}, ExerciseName: {reader.GetString(reader.GetOrdinal("ExerciseName"))}, ...");

                            ExercisePlanModel exercise = new ExercisePlanModel
                            {
                                ExerciseID = reader.GetInt32(reader.GetOrdinal("ExerciseID")),
                                MuscleID = reader.GetInt32(reader.GetOrdinal("MuscleID")),
                                ExerciseName = reader.GetString(reader.GetOrdinal("ExerciseName")),
                                ExerciseImageReference = reader.GetString(reader.GetOrdinal("ExerciseImageReference")),
                                Description = reader.GetString(reader.GetOrdinal("Description")),
                                Time = reader.GetInt32(reader.GetOrdinal("Time")),
                                Raps = reader.GetInt32(reader.GetOrdinal("Raps"))
                            };

                            exercises.Add(exercise);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (log, display an error message, etc.)
                Console.WriteLine($"Error retrieving exercise information: {ex.Message}");
            }

            return exercises;
        }



    }
}
