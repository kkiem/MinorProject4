namespace MinorProject4.Models
{
    public class Team
    {
        public int TeamID { get; set; }
        public string TeamName { get; set; } = "";
        public string BaseCountry { get; set; } = "";
        public string EngineSupplier { get; set; } = "";
    }

    public class Driver
    {
        public int DriverID { get; set; }
        public string DriverName { get; set; } = "";
        public string Nationality { get; set; } = "";
        public int CarNumber { get; set; }
        public int? TeamID { get; set; }
        public string TeamName { get; set; } = "";
    }

    public class Principal
    {
        public int PrincipalID { get; set; }
        public string PrincipalName { get; set; } = "";
        public string RoleTitle { get; set; } = "";
        public int YearsExperience { get; set; }
        public int? TeamID { get; set; }
        public string TeamName { get; set; } = "";
    }
}