import { Injectable } from '@angular/core';
import { User } from '../../implementations/User/User';
import { IReqError } from '../../interfaces/ReqError/IReqError';
import { IBook } from '../../interfaces/Book/IBook';
import { IAuthor } from '../../interfaces/Author/IAuthor';
import { Subject } from 'rxjs';
import { IUser } from '../../interfaces/User/IUser';

@Injectable({
  providedIn: 'root'
})
export class DataExchangeService {

private userTransfer! : User;
public errorTransfer! : IReqError;  

private books : IBook[] = []
private authors : IAuthor[] = []

public EditBook : Subject<number> = new Subject<number>()
public EditAuthor : Subject<number> = new Subject<number>()

deleteUser()
{
  this.userTransfer = new User();
}

getUser() : IUser
{
  return this.userTransfer;
}

setUser(user : IUser)
{
  this.userTransfer = user;
}

addBook(book : IBook)
{
  this.books.push(book);
}

updateBooks(books : IBook[])
{
  this.books = books;
}

getAllBooks() : IBook[]
{
  return this.books;
}

getBookById(id : number) : IBook | undefined
{
  return this.books.find((x, i) => x.id === id);
}

editBook(book : IBook)
{
  let i = this.books.indexOf(book);
  this.books[i] = book;
}

deleteBook(id : number)
{
  this.books = this.books.filter((x, i) => x.id != id);
}

updateAllAuthors(authors : IAuthor[])
{
  this.authors = authors;
}

getAllAuthors() : IAuthor[]
{
  return this.authors;
}

getAuthorById(id : number) : IAuthor | undefined
{
  return this.authors.find((x, i) => x.id === id);
}

addAuthor(author : IAuthor)
{
  this.authors.push(author);
}

editAuthor(author : IAuthor)
{
  let i = this.authors.indexOf(author);
  this.authors[i] = author;
}

deleteAuthor(id : number)
{
  this.authors = this.authors.filter((x, i) => x.id != id);
}

}
