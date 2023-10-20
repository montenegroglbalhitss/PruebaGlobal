namespace PreubaLogics.Models
{
    public class PersonaDto
    {
        public short Id { get; set; }

        public required string Username { get; set; }


        public required string Cedula { get; set; }

        public required string FirstName { get; set; }

        public required string MidleName { get; set; }

        public required string LastName { get; set; }

        public required string LastNameSecond { get; set; }

        public required string RazonSocial { get; set; }
    }
    public class PersonaRequest: PaginatedRequest
    {

        public  string? Username { get; set; }

        public  string? Cedula { get; set; }

        public  string? FirstName { get; set; }

        public  string? MidleName { get; set; }

        public   string? LastName { get; set; }

        public   string? LastNameSecond { get; set; }

        public   string? RazonSocial { get; set; }
  
    }
    public class PaginatedRequest
    {
        public int Offset { get; set; } = 1;
        public int Take { get; set; } = 10;
        public string? Sort { get; set; }
    }

}
