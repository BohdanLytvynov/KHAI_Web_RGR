import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Author } from '../../implementations/Author/Author';
import { User } from '../../implementations/User/User';
import { ILoginUser } from '../../interfaces/LoginUser/ILoginUser';
import { Book } from '../../implementations/Book/Book';
import { IBook } from '../../interfaces/Book/IBook';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  constructor() { }

  //private authors: Author[] = authors;
  //private books: Book[] = books;
  private ApiHttp = "http://localhost:5154/api/";
  private ApiHttps : string = "https://localhost:7230/api/";
  private httpClient : HttpClient = inject(HttpClient);
  //Books
  getBooks<T>(): Observable<T> {   
    return this.httpClient.get<T>(this.ApiHttp + "Books/GetAll",
      {
        headers: { "Content-Type": "application/json" },
        withCredentials: true,
      }
    );    
  };

  addBook(book : Book) : Observable<any>
  {
    return this.httpClient.post( this.ApiHttp + "Books/Create", book,
      {
        headers: { "Content-Type": "application/json" },
        withCredentials: true,
      }
    )
  }

  deleteBook(id : number) : Observable<any>
  {
    return this.httpClient.delete(this.ApiHttp + `Books/Delete/${id}`,
      {
        withCredentials : true
      }
    );
  }

  editBook(book : IBook) : Observable<any>
  {
    return this.httpClient.put<IBook>(this.ApiHttp + "Books/Edit", book,
      {
        headers: { "Content-Type": "application/json" },
        withCredentials: true,
      }
    );
  }
  //Authors
  getAuthors(): Observable<any> {
    return this.httpClient.get(this.ApiHttp + "Authors/GetAll",
      {
        headers: { "Content-Type": "application/json" },
        withCredentials: true
      }
      
    );    
  };

  addAuthor(author : Author) : Observable<any>
  {
    return this.httpClient.post(this.ApiHttp + "Authors/Create", author,
      {
        headers: { "Content-Type": "application/json" },
        withCredentials: true
      }
    )
  }
  
  editAuthor(author : Author) : Observable<any>
  {
    return this.httpClient.put(this.ApiHttp + "Authors/Edit", author,
      {
        headers: {"Content-Type" : "application/json"},
        withCredentials: true
      }
    );
  }

  deleteAuthor(id : number) : Observable<any>
  {
    return this.httpClient.delete(this.ApiHttp + `Authors/Delete/${id}`,
      {
        headers: { "Content-Type": "application/json" },
        withCredentials : true
      }
    )
  }
  
  //Users
  registerUser<T>(user: User) : Observable<T> {                    
    return this.httpClient.post<T>(this.ApiHttp + "Accounts/Register", user, 
      { 
        headers: { "Content-Type": "application/json" } 
      });          
  };

  loginUser<T>(user : ILoginUser) : Observable<T>
  {      
      return this.httpClient.post<T>(this.ApiHttp + "Accounts/Login", user,
        {
          headers: { "Content-Type": "application/json"},
          withCredentials: true,           
        }
      );
  }

  logoutUser() : Observable<object>
  {
    return this.httpClient.get(this.ApiHttp + "Accounts/LogOut", 
      {
        withCredentials: true,
      }
    );
  }
  
}
