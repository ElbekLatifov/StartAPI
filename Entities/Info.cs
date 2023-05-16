namespace Api.Entities;

public class Info
{
    public string Name { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public ushort Age { get; set; }
    public string UserName { get; set; }
    public bool IsMarried { get; set; }
    public bool Worker { get; set; }
    public List<string> Azolar { get; set; }
    public Study Study { get; set; }

}

public class Study
{
    public string Name { get; set; }
    public ushort Kurs { get; set; }
    public string Universitet { get; set; }
    public bool Zakonchilsiya { get; set; }
    public List<string> Kursdoshlar { get; set; }
}