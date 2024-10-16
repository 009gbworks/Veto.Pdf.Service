using DinkToPdf;
using DinkToPdf.Contracts;
using System.Runtime.InteropServices;
using Veto.Pdf.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
{
    var assemblyLoadContext = new CustomAssemblyLoadContext();
    assemblyLoadContext.LoadDinkToPdfUnmanagedLibrary();
}


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
