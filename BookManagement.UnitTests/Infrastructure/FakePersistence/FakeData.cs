using BookManagement.Core.Entities;
using BookManagement.Core.Repositories;
using BookManagement.Infrastructure.Persistence;
using BookManagement.Infrastructure.Persistence.Repositories;
using BookManagement.UnitTests.Infrastructure.FakePersistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.UnitTests.Infrastructure.FakePersistence;
public class FakeData
{
    private readonly IBookRepository _bookRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILoanRepository _loanRepository;

    public FakeData()
    {
        _bookRepository = new FakeBookRepository();
        _userRepository = new FakeUserRepository();
        _loanRepository = new FakeLoanRepository();
        Task task = GenerateAllData();
    }

    private async Task GenerateAllData()
    {
        //Add Books
        var book1 = new Book("Compiladores: Princípios, Técnicas e Ferramentas", "Alfred V. Aho", "978-8588639249", 2007);
        var book2 = new Book("Algoritmos - Teoria e Prática", "Thomas H. Cormen", "978-8535236996", 2012);
        var book3 = new Book("Arquitetura e Organização de Computadores", "William Stallings", "978-8543020532", 2017);
        var book4 = new Book("Sistemas Operacionais Modernos", "Andrew S. Tanenbaum", "978-8543005676", 2015);
        var book5 = new Book("Redes de Computadores", "Andrew Tanenbaum", "978-8582605608", 2011);

        book4.ToActive(false);

        await _bookRepository.CreateBookAsync(book1);
        await _bookRepository.CreateBookAsync(book2);
        await _bookRepository.CreateBookAsync(book3);
        await _bookRepository.CreateBookAsync(book4);
        await _bookRepository.CreateBookAsync(book5);


        //Add BookStocks Quantity
        var stock1 = book1.Stock;
        var stock2 = book2.Stock;
        var stock3 = book3.Stock;
        var stock4 = book4.Stock;
        var stock5 = book5.Stock;

        stock1.Update(3);
        stock2.Update(4);
        stock3.Update(1);
        stock4.Update(2);
        stock5.Update(3);

        await _bookRepository.UpdateBookStockAsync(stock1);
        await _bookRepository.UpdateBookStockAsync(stock2);
        await _bookRepository.UpdateBookStockAsync(stock3);
        await _bookRepository.UpdateBookStockAsync(stock4);
        await _bookRepository.UpdateBookStockAsync(stock5);


    
        //Add Users
        var user1 = new User("Andre Silva", "andre@gmail.com", "1Q2w3e4R#!", "user", new DateTime(1979, 10, 7));
        var user2 = new User("Bruno Carvalho", "carvalho@gmail.com", "1Q2w3e4R#!", "user", new DateTime(1981, 4, 5));
        var user3 = new User("Fernanda Silva", "fernanda@gmail.com", "1Q2w3e4R#!", "user", new DateTime(1990, 7, 2));
        var user4 = new User("Alexandre Benevides", "bruno@gmail.com", "1Q2w3e4R#!", "user", new DateTime(1985, 1, 15));
        var user5 = new User("Rodrigo Farias", "rodrigo@gmail.com", "1Q2w3e4R#!", "user", new DateTime(1983, 8, 10));
        var admin = new User("admin", "admin@email.com", "1Q2w3e4R#!", "admin", new DateTime(1990, 1, 1));

        user4.ToActive(false);

        await _userRepository.CreateUserAsync(user1);
        await _userRepository.CreateUserAsync(user2);
        await _userRepository.CreateUserAsync(user3);
        await _userRepository.CreateUserAsync(user4);
        await _userRepository.CreateUserAsync(user5);
        await _userRepository.CreateUserAsync(admin);



        //Add Loan
        var loan1 = new Loan(user1.Id, book2.Id);
        book2.Stock.LoanStockMovement();
        await _bookRepository.UpdateBookStockAsync(book2.Stock);

        var loan2 = new Loan(user2.Id, book5.Id);
        book5.Stock.LoanStockMovement();
        await _bookRepository.UpdateBookStockAsync(book5.Stock);

        var loan3 = new Loan(user1.Id, book3.Id);
        book3.Stock.LoanStockMovement();
        await _bookRepository.UpdateBookStockAsync(book3.Stock);

        var loan4 = new Loan(user2.Id, book1.Id);
        book1.Stock.LoanStockMovement();
        await _bookRepository.UpdateBookStockAsync(book1.Stock);

        var loan5 = new Loan(user2.Id, book2.Id);
        book2.Stock.LoanStockMovement();
        await _bookRepository.UpdateBookStockAsync(book2.Stock);

        var loan6 = new Loan(user3.Id, book5.Id);
        book5.Stock.LoanStockMovement();
        await _bookRepository.UpdateBookStockAsync(book5.Stock);

        await _loanRepository.CreateLoanAsync(loan1);
        await _loanRepository.CreateLoanAsync(loan2);
        await _loanRepository.CreateLoanAsync(loan3);
        await _loanRepository.CreateLoanAsync(loan4);
        await _loanRepository.CreateLoanAsync(loan5);
        await _loanRepository.CreateLoanAsync(loan6);


        loan4.LoanReturned();
        book1.Stock.ReturnStockMovement();
        await _bookRepository.UpdateBookStockAsync(book1.Stock);
        await _loanRepository.UpdateLoanAsync(loan4);

        
    }
}
