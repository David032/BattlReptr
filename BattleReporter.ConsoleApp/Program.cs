using BattleReporter.Core;
using BattleReporter.ListManagement;

Console.WriteLine("Please provide full path to location of army list:");
string? pathToFile = Console.ReadLine();
Console.WriteLine("Please select the desired output: 1 - XML 2 - JSON:");
int? mode = int.Parse(Console.ReadLine());

if (pathToFile != null)
{
    ListFormatter formatter = new ListFormatter();
    switch (mode)
    {
        case 1:
            formatter.OutputListAsXML(pathToFile);
            break;
        case 2:
            formatter.OutputListAsJSON(pathToFile);
            break;
        default:
            break;
    }
}
