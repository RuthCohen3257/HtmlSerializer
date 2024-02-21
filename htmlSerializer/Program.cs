
using htmlSerializer;
using System.Text.RegularExpressions;

//למורה היקרה! אנחנו הרצנו ובדקנו את הקוד עם נקודת עצירה(דיבאג) ולא עשינו הדפסות... תודה!ובדיקה נעימה
 var html = await Load("https://hebrewbooks.org/beis");

HtmlElement htmlElement1 = new HtmlElement();
HtmlParser parser = new HtmlParser();
htmlElement1 = parser.Serialize(html);
Console.WriteLine("----Serialize----");

Selector selector = new Selector();
//selector = Selector.BuildSel("div p.class-name.claas-sari&ruthy");
Console.WriteLine("-----selector-----");

//IEnumerable<HtmlElement> l = htmlElement1.Descendants();
Console.WriteLine("----Descendants----");

//IEnumerable<HtmlElement> l1 = htmlElement1.Ancestors();//מכיון שהפונקציה עובדת עם yeild והפונקציה לא שומשה לאף אחד אז מיד עברה להדפסה
Console.WriteLine("----Ancestors----");


//selector:
string queryString1 = "#pnlMenubar table div.inactBG";//5 results
string queryString2 = "div tr.oddrow td a";//1866 results
string queryString3 = "tr td div#cpMstr_PanelSeforim";//only one result
selector = Selector.BuildSel(queryString1);
var elementsList = htmlElement1.FindElementsBySelector(selector);
Console.WriteLine("----FindElementsBySelector----");



static async Task<string> Load(string url)
{
    HttpClient client = new HttpClient();
    var response = await client.GetAsync(url);
    var html = await response.Content.ReadAsStringAsync();
    return html;
}
