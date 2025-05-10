using Microsoft.EntityFrameworkCore;
using MinimalApi_Training;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ToDoDb>(opt =>
{
    opt.UseInMemoryDatabase("TodoList");
});
 

var app = builder.Build();
app.MapGet("/todoitems", async (ToDoDb db) => await db.DoItems.ToListAsync());

app.MapGet("/todoitems/{id}", async (int id, ToDoDb db) =>
{
    await db.DoItems.FindAsync(id);
});
app.MapPost("/todoitems", async (ToDoItem item, ToDoDb db) =>
{
    db.DoItems.Add(item);
    await db.SaveChangesAsync();
    return Results.Created($"/todoitems/{item.Id}", item);  
});
app.MapPut("/todoitems/{id}", async (int id, ToDoItem inputItem, ToDoDb db) =>
{
    var item = await db.DoItems.FindAsync(id);
    if (item is null) return Results.NotFound();
    item.Name = inputItem.Name;
    item.IsCompleted = inputItem.IsCompleted;
    await db.SaveChangesAsync();
    return Results.NoContent();
});
app.MapDelete("/todoitems/{id}", async (int id, ToDoDb db) =>
{
    var item = await db.DoItems.FindAsync(id);
    if (item is null) return Results.NotFound();
    db.DoItems.Remove(item);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();


