using Microsoft.Data.SqlClient;
using MinorProject4.Models;

namespace MinorProject4
{
    public class DataAccess
    {
        private readonly string _connStr;

        public DataAccess(IConfiguration config)
        {
            _connStr = config.GetConnectionString("DefaultConnection") ?? "";
        }

        // ================= TEAMS (Admin Table) =================
        public List<Team> GetTeams()
        {
            var list = new List<Team>();
            using var conn = new SqlConnection(_connStr);
            conn.Open();
            using var cmd = new SqlCommand("SELECT TeamID, TeamName, BaseCountry, EngineSupplier FROM Teams", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Team
                {
                    TeamID = reader.GetInt32(0),
                    TeamName = reader.GetString(1),
                    BaseCountry = reader.GetString(2),
                    EngineSupplier = reader.GetString(3)
                });
            }
            return list;
        }

        public void AddTeam(string name, string country, string engine)
        {
            using var conn = new SqlConnection(_connStr);
            conn.Open();
            using var cmd = new SqlCommand("INSERT INTO Teams (TeamName, BaseCountry, EngineSupplier) VALUES (@n, @c, @e)", conn);
            cmd.Parameters.AddWithValue("@n", name);
            cmd.Parameters.AddWithValue("@c", country);
            cmd.Parameters.AddWithValue("@e", engine);
            cmd.ExecuteNonQuery();
        }

        public void UpdateTeam(int id, string name, string country, string engine)
        {
            using var conn = new SqlConnection(_connStr);
            conn.Open();
            using var cmd = new SqlCommand("UPDATE Teams SET TeamName = @n, BaseCountry = @c, EngineSupplier = @e WHERE TeamID = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@n", name);
            cmd.Parameters.AddWithValue("@c", country);
            cmd.Parameters.AddWithValue("@e", engine);
            cmd.ExecuteNonQuery();
        }

        public void DeleteTeam(int id)
        {
            using var conn = new SqlConnection(_connStr);
            conn.Open();
            using var cmd = new SqlCommand("DELETE FROM Teams WHERE TeamID = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        // ================= DRIVERS (With 2-Field Search) =================
        public List<Driver> GetDrivers(string? search = null)
        {
            var list = new List<Driver>();
            using var conn = new SqlConnection(_connStr);
            conn.Open();

            string sql = @"SELECT d.DriverID, d.DriverName, d.Nationality, d.CarNumber, d.TeamID, ISNULL(t.TeamName, 'None')
                           FROM Drivers d
                           LEFT JOIN Teams t ON d.TeamID = t.TeamID";

            if (!string.IsNullOrWhiteSpace(search))
            {
                sql += " WHERE d.DriverName LIKE @s OR d.Nationality LIKE @s";
            }

            using var cmd = new SqlCommand(sql, conn);
            if (!string.IsNullOrWhiteSpace(search))
            {
                cmd.Parameters.AddWithValue("@s", "%" + search + "%");
            }

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Driver
                {
                    DriverID = reader.GetInt32(0),
                    DriverName = reader.GetString(1),
                    Nationality = reader.GetString(2),
                    CarNumber = reader.GetInt32(3),
                    TeamID = reader.IsDBNull(4) ? null : reader.GetInt32(4),
                    TeamName = reader.GetString(5)
                });
            }
            return list;
        }

        public void AddDriver(string name, string nationality, int carNumber, int? teamId)
        {
            using var conn = new SqlConnection(_connStr);
            conn.Open();
            using var cmd = new SqlCommand("INSERT INTO Drivers (DriverName, Nationality, CarNumber, TeamID) VALUES (@n, @nat, @num, @t)", conn);
            cmd.Parameters.AddWithValue("@n", name);
            cmd.Parameters.AddWithValue("@nat", nationality);
            cmd.Parameters.AddWithValue("@num", carNumber);
            cmd.Parameters.AddWithValue("@t", (object?)teamId ?? DBNull.Value);
            cmd.ExecuteNonQuery();
        }

        public void UpdateDriver(int id, string name, string nationality, int carNumber, int? teamId)
        {
            using var conn = new SqlConnection(_connStr);
            conn.Open();
            using var cmd = new SqlCommand("UPDATE Drivers SET DriverName = @n, Nationality = @nat, CarNumber = @num, TeamID = @t WHERE DriverID = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@n", name);
            cmd.Parameters.AddWithValue("@nat", nationality);
            cmd.Parameters.AddWithValue("@num", carNumber);
            cmd.Parameters.AddWithValue("@t", (object?)teamId ?? DBNull.Value);
            cmd.ExecuteNonQuery();
        }

        public void DeleteDriver(int id)
        {
            using var conn = new SqlConnection(_connStr);
            conn.Open();
            using var cmd = new SqlCommand("DELETE FROM Drivers WHERE DriverID = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        // ================= PRINCIPALS =================
        public List<Principal> GetPrincipals()
        {
            var list = new List<Principal>();
            using var conn = new SqlConnection(_connStr);
            conn.Open();
            string sql = @"SELECT p.PrincipalID, p.PrincipalName, p.RoleTitle, p.YearsExperience, p.TeamID, ISNULL(t.TeamName, 'None')
                           FROM Principals p
                           LEFT JOIN Teams t ON p.TeamID = t.TeamID";
            using var cmd = new SqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Principal
                {
                    PrincipalID = reader.GetInt32(0),
                    PrincipalName = reader.GetString(1),
                    RoleTitle = reader.GetString(2),
                    YearsExperience = reader.GetInt32(3),
                    TeamID = reader.IsDBNull(4) ? null : reader.GetInt32(4),
                    TeamName = reader.GetString(5)
                });
            }
            return list;
        }

        public void AddPrincipal(string name, string role, int years, int? teamId)
        {
            using var conn = new SqlConnection(_connStr);
            conn.Open();
            using var cmd = new SqlCommand("INSERT INTO Principals (PrincipalName, RoleTitle, YearsExperience, TeamID) VALUES (@n, @r, @y, @t)", conn);
            cmd.Parameters.AddWithValue("@n", name);
            cmd.Parameters.AddWithValue("@r", role);
            cmd.Parameters.AddWithValue("@y", years);
            cmd.Parameters.AddWithValue("@t", (object?)teamId ?? DBNull.Value);
            cmd.ExecuteNonQuery();
        }

        public void AddPrincipal(string name, int years, int? teamId) =>
            AddPrincipal(name, "Team Principal", years, teamId);

        public void UpdatePrincipal(int id, string name, string role, int years, int? teamId)
        {
            using var conn = new SqlConnection(_connStr);
            conn.Open();
            using var cmd = new SqlCommand("UPDATE Principals SET PrincipalName = @n, RoleTitle = @r, YearsExperience = @y, TeamID = @t WHERE PrincipalID = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@n", name);
            cmd.Parameters.AddWithValue("@r", role);
            cmd.Parameters.AddWithValue("@y", years);
            cmd.Parameters.AddWithValue("@t", (object?)teamId ?? DBNull.Value);
            cmd.ExecuteNonQuery();
        }

        public void UpdatePrincipal(int id, string name, int years, int? teamId) =>
            UpdatePrincipal(id, name, "Team Principal", years, teamId);

        public void DeletePrincipal(int id)
        {
            using var conn = new SqlConnection(_connStr);
            conn.Open();
            using var cmd = new SqlCommand("DELETE FROM Principals WHERE PrincipalID = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
}