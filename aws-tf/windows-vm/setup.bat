cd c:\Users\Administrator
mkdir apps
cd apps
curl -L -O https://download.visualstudio.microsoft.com/download/pr/44069ee2-ce02-41d7-bcc5-2168a1653278/cfc5131c81ae00a5f77f05f9963ec98d/dotnet-sdk-5.0.404-win-x64.exe
dotnet-sdk-5.0.404-win-x64.exe
curl -L -O https://github.com/cloudfirst-dev/dotnet-products/archive/refs/heads/master.zip
powershell -command "Expand-Archive C:\Users\Administrator\apps\master.zip C:\Users\Administrator\apps"
ren dotnet-products-master dotnet-products
netsh advfirewall firewall add rule name= "Open Port 5003" dir=in action=allow protocol=TCP localport=5003
netsh advfirewall firewall add rule name= "Open Port 3000" dir=in action=allow protocol=TCP localport=3000
curl -L -O https://github.com/open-policy-agent/opa/releases/download/v0.36.1/opa_windows_amd64.exe
ren opa_windows_amd64.exe opa.exe
dotnet dev-certs https --trust
curl -L -O https://nodejs.org/dist/v16.13.2/node-v16.13.2-x64.msi
node-v16.13.2-x64.msi