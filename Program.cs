var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
//{step2}}
var books = new List<Book>
{
    new Book {bookId=1, bookTitle="Javascript Evolution",bookAuthor="Thomas"},
    new Book {bookId=2, bookTitle=".NET Evolution",bookAuthor="Alex"},
    
};
//{step 3}
app.MapGet("/book",()=>
{
    return books;

});
//{step4}
app.MapGet("/book/{id}",(int Id)=>
{
    var book = books.Find(b=>b.bookId==Id);
    if (book is null) 
    return Results.NotFound("sorry the book doesn't exist");
    
    return Results.Ok(book);

});

//step 5
app.MapPost("/post",(Book newBook)=>
{
    books.Add(newBook);
    return books;

});

//step 6 
app.MapPut("/book/{id}",(Book updateBook, int Id)=>
{
    var book = books.Find(b=>b.bookId==Id);
    if (book is null) 
    return Results.NotFound("sorry the book doesn't exist");
    book.bookTitle = updateBook.bookTitle; 
    book.bookAuthor = updateBook.bookAuthor;

    return Results.Ok(books);

});
//step 7 
app.MapDelete("/book/{id}",(int Id)=>
{
    var book = books.Find(b=>b.bookId==Id);
    if (book is null) 
    return Results.NotFound("sorry the book doesn't exist");
    books.Remove(book);
    return Results.Ok(books);

});

app.Run();

//{step1}
class Book
{
    public int bookId { get; set; }
    public  string bookTitle { get; set; } = string.Empty;
    public string  bookAuthor { get; set; } = string.Empty;
}
