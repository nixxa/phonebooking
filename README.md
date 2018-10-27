# Simple web application, allowing booking and releasing phones

## Deploy procedure
1. Clone repo
2. Change directory to the repo root
3. Run `npm install`
4. Install .NET SDK 2.1
5. Change directory to server-side root `cd ./api`
6. Run build `dotnet build`
7. Change directory to webapi root `cd ./webapi`
8. Run webapp in background using `dotnet run &`
9. Change current directory to project root `cd ../../`
10. Run `npm run all` for UI with server-side or just `npm run dev` for only UI.