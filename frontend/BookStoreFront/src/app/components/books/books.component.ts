import { Component, EventEmitter, Inject, Output } from '@angular/core';
import { BookFormComponent } from '../book-form/book-form.component';
import { DataService } from '../../services/data-service/data.service';
import { BookEditFormComponent } from '../book-edit-form/book-edit-form.component';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { DataExchangeService } from '../../services/data-exchange/data-exchange.service';
import { Unauthorized } from '../../implementations/UnAuthorized/UnAuthorized';
import { IErrorHandler } from '../../interfaces/ErrorHandler/IErrorHandler';
import { ErrorHandler } from '../../implementations/ErrorHandler/ErrorHandler';
import { IBook } from '../../interfaces/Book/IBook';
import { Book } from '../../implementations/Book/Book';

@Component({
  selector: 'app-books',
  standalone: true,
  imports: [BookFormComponent, BookEditFormComponent, CommonModule],
  providers: [DataService],
  templateUrl: './books.component.html',
  styleUrl: './books.component.css'
})
export class BooksComponent {

  public readonly DataExchange! : DataExchangeService;

  errorHandler : IErrorHandler = new ErrorHandler;

  constructor(@Inject(DataService) private dataService: DataService,
  @Inject(Router) private router : Router,
  @Inject(DataExchangeService) private dataExchange : DataExchangeService) {
    this.DataExchange = dataExchange
  };
  
  @Output() onEditPress : EventEmitter<number> = new EventEmitter<number>();

  showBookForm = false;
  bookForm = false;
  editForm : boolean = false;

  showBookFormComponent() {
    this.bookForm = true;
    this.showBookForm = true;
  };

  hideBookFormComponent(value: boolean) {
    this.showBookForm = value;
  };

  showBookEditForm = false;
  bookEditForm = false;
  currentID = 0;

  //Connection established
  editFormInitialized()
  {
    this.DataExchange.EditBook.next(this.currentID)
    this.editForm = true;
  }

  showBookEditFormComponent(id: number) {       
    this.currentID = id;
    this.bookEditForm = true;
    this.showBookEditForm = true;

    if(this.editForm)
    {
      this.DataExchange.EditBook.next(this.currentID)
    }
  }

  hideBookEditFormComponent() {
    this.showBookEditForm = false;
  }
 
  getBooks()
  {
    this.dataService.getBooks<any>().subscribe((resp) => 
      {
          if(resp["message"] == "Unauthorized")
          {
            this.dataExchange.errorTransfer = { error: resp["message"], action : 'Get books',
              route : "/"
            }
            this.router.navigate(["/reg-fail"]);
          }
          else
          {
            this.dataExchange.updateBooks(resp as Book[])
            //this.items = resp as Book[];
          }
      }, 
      err => {
        this.dataExchange.errorTransfer = this.errorHandler.Handle(err, 'Get books', "/main");
        this.router.navigate(["/reg-fail"]);
      })  
  }

  ngOnInit() {
    this.getBooks();
  };

  deleteBook(id: number) {

    this.dataService.deleteBook(id).subscribe(
      (resp) => { 

        if(resp["message"] == "Unauthorized")
          {
            this.dataExchange.errorTransfer = { error: resp["message"], action : 'Get books',
              route : "/"
             }
            this.router.navigate(["/reg-fail"]);
          }
          else
          {            
            this.dataExchange.deleteBook(Number(resp["id"]));
          }

       },
      err => {
        this.dataExchange.errorTransfer = this.errorHandler
        .Handle(err, `Delete book with id ${id}`, "/main");
        this.router.navigate(["/reg-fail"]);
      })      
    
  };

}
