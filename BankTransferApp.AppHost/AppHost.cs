var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.BankTransferApp_Api>("banktransferapp-api");

builder.Build().Run();
