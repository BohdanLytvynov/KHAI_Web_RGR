import { CommonModule, NgClass } from '@angular/common';
import { Component, ElementRef, EventEmitter, Inject, Input, OnInit, Output, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ValidationService } from '../../services/validation/validation.service';
import { ValidatorBase } from '../../services/validation/validation';
import { DataService } from '../../services/data-service/data.service';
import { Book } from '../../implementations/Book/Book';
import { Genre } from '../../implementations/Genre/Genre';
import { Router } from '@angular/router';
import { DataExchangeService } from '../../services/data-exchange/data-exchange.service';
import { IErrorHandler } from '../../interfaces/ErrorHandler/IErrorHandler';
import { ErrorHandler } from '../../implementations/ErrorHandler/ErrorHandler';
import { IBook } from '../../interfaces/Book/IBook';

@Component({
  selector: 'app-book-form',
  standalone: true,
  imports: [NgClass, FormsModule, CommonModule],
  templateUrl: './book-form.component.html',
  styleUrl: './book-form.component.css'
})
export class BookFormComponent extends ValidatorBase implements OnInit  {

  @Input() show = false;
  @Output() onChange = new EventEmitter<boolean>();
  @ViewChild('Submit', { static: false }) Submit! : ElementRef

  handleClose(value: boolean) {
    this.onChange.emit(value)
  }
  
constructor(@Inject(ValidationService) private validation : ValidationService,
@Inject(DataService) private data : DataService,
@Inject(DataExchangeService) private dataExchange : DataExchangeService,
@Inject(Router) private router : Router)
{
  super();
}
  ngOnInit(): void {
    this.Init(3);    
  }
  errorHandler : IErrorHandler = new ErrorHandler;
  bookName: string = '';
  bookYear: string = '';
  bookGenre: string = '';

  all_fields_correct : boolean = false;  
  
  addBook() {
        
    if(!this.all_fields_correct)
      return;

    let genr : string[] = this.bookGenre.split(',');

    let geners : Genre[] = []
    
    for(let g of genr)
    {
      geners.unshift({ id:0, name : g });
    }
    
    let book : Book = { id : 0, name: this.bookName, 
      geners : geners, pubYear : Number(this.bookYear)  }

    this.data.addBook(book).subscribe(
      (resp) => {

        if(resp["message"] == "Unauthorized")
          {
            this.dataExchange.errorTransfer = { error: resp["message"], action : 'Add Book', 
              route : "/"
            }
            this.router.navigate(["/reg-fail"]);
          }
          else
          {
             this.dataExchange.addBook(resp as IBook)
          }

        this.handleClean();
        this.handleClose(false)
      },
      err => {
        this.dataExchange.errorTransfer = this.errorHandler.Handle(err, 'Add Book', "/main");
        this.router.navigate(["/reg-fail"]);
      }
    );

    
  }

  handleClean() {
    
    this.bookName = '';
    this.bookYear = '';
    this.bookGenre = '';
    this.all_fields_correct = false;
    this.ResetValidArray();
    this.enableElement(this.all_fields_correct, this.Submit)
  }

  //Validation 
onBookNameChange(value : string)
{
  this.validArray[0] = this.validation.ValidateTextNotEmpty(value);
  this.all_fields_correct = this.CheckValidArray();
  this.enableElement(this.all_fields_correct, this.Submit)
}

onBookYearChange(value : string)
{
  this.validArray[1] = this.validation.ValidateNumber(value);
  this.all_fields_correct = this.CheckValidArray();
  this.enableElement(this.all_fields_correct, this.Submit)
}

onBookGenreChanged(value : string)
{
  this.validArray[2] = this.validation.VaidateSequence(value);
  this.all_fields_correct = this.CheckValidArray();
  this.enableElement(this.all_fields_correct, this.Submit)
}



}
