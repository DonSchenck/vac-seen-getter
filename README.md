# vac-seen-getter
Retrieves vaccination summary for a date range

oc new-app --name=vac-seen-getter dotnet:6.0~https://github.com/donschenck/vac-seen-getter -e MYSQL_CONNECTION_STRING="Server=mariadb;User ID=root;Password=admin;Database=vaxdb;"

oc expose service vac-seen-getter