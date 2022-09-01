# vac-seen-getter
Retrieves vaccination summary. Starts at the date supplied and goes back in time the number of days supplied.

For example: 20220901, 30 will begin at 9/1/2022 and summarize each day up to 30 days prior to that date.

Here's an example of calling this API (from a Blazor server-side app):

string queryDate = DateTime.Now.ToString("yyyyMMdd");
string country = "US";
string URI = $"http://vac-seen-getter:8080/vaccination/GetVaccinationSummary/?queryDate={queryDate}&countryCode={country}&DaysToGoBack=30";

## Creating this app in OpenShift
Run the following command:

`oc new-app --name=vac-seen-getter dotnet:6.0~https://github.com/donschenck/vac-seen-getter -e MYSQL_CONNECTION_STRING="Server=mariadb;User ID=root;Password=admin;Database=vaxdb;"`
