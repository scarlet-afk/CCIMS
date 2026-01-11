using CCIMS.Models;

public class Suspect
{
    public int SuspectID { get; set; }
    public int CaseID { get; set; }
    public string Name { get; set;}
    public int Age { get; set; }
    public string Gender { get; set; }

    public string Notes { get; set; }
    public Case Case { get; set; }
}