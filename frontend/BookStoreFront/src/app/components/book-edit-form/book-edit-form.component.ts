import { NgClass } from '@angular/common';
import { Component, ElementRef, EventEmitter, Inject, Input, OnInit, Output, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { DataService } from '../../services/data-service/data.service';
import { IBook } from '../../interfaces/Book/IBook';
import { DataExchangeService } from '../../services/data-exchange/data-exchange.service';
import { ValidationService } from '../../services/validation/validation.service';
import { IErrorHandler } from '../../interfaces/ErrorHandler/IErrorHandler';
import { ErrorHandler } from '../../implementations/ErrorHandler/ErrorHandler';
import { ValidatorBase } from '../../services/validation/validation';
import { Router } from '@angular/router';
import { Book } from '../../implementations/Book/Book';

@Component({
  selector: 'app-book-edit-form',
  standalone: true,
  imports: [NgClass, FormsModule],
  providers: [DataService],
  templateUrl: './book-edit-form.component.html',
  styleUrl: './book-edit-form.component.css'
})
export class BookEditFormComponent extends ValidatorBase implements OnInit{

  constructor(@Inject(DataExchangeService) private dataExchange: DataExchangeService,
  @Inject(DataService) private dataService : DataService,
  @Inject(ValidationService) private validation : ValidationService,
  @Inject(Router) private router : Router) {
    super();
  }

  @Input() show = false;
  @Output() onChange = new EventEmitter<boolean>();
  @Output() onInit = new EventEmitter();
  @Input() id : number = 0

  currentBook! : IBook 

  @ViewChild('Submit', { static: false }) Submit! : ElementRef
  
  handleClose(value: boolean) {
    this.onChange.emit(value)
  };

  errorHandler : IErrorHandler = new ErrorHandler();
  all_field_correct : boolean = true;
  genres : string = '';
  

  ngOnInit() {
    this.Init(3)
    this.currentBook = this.dataExchange.getBookById(this.id)!; 
    this.validArray.fill(true);    
       
    this.dataExchange.EditBook.subscribe(
      (id) => { this.currentBook = this.dataExchange.getBookById(id)! 
        this.genres = ''
        for(let g of this.currentBook.geners)
          this.genres += g.name + ","
      }
    );  

    this.onInit.emit(); 
  };
    
  editBook() {

    if(!this.all_field_correct)
      return;
    
    this.dataService.editBook(this.currentBook)
    .subscribe(
      resp => {
        if(resp["message"] == "Unauthorized")
          {
            this.dataExchange.errorTransfer = { error: resp["message"], action : 'Edit book',
              route : "/"
             }
            this.router.navigate(["/reg-fail"]);
          }
          else
          {            
            this.dataExchange.editBook(resp as Book)
          }
      },
      err => {
        this.dataExchange.errorTransfer = this.errorHandler.Handle(err, 'Edit Book', "/main");
        this.router.navigate(["/reg-fail"]);
      }
      
    );
    this.handleClose(false)
  };

  onNameChanged(value : string)
  {
    this.validArray[0] = this.validation.ValidateTextNotEmpty(value);
    this.all_field_correct = this.CheckValidArray();
    this.enableElement(this.all_field_correct, this.Submit)
  }

  onPubYearChanged(value : string)
  {
    this.validArray[1] = this.validation.ValidateNumber(value);
    this.all_field_correct = this.CheckValidArray();
    this.enableElement(this.all_field_correct, this.Submit)
  }

  onGenresChanged(value : string)
  {
    this.validArray[2] = this.validation.VaidateSequence(value);
    this.all_field_correct = this.CheckValidArray();
    this.enableElement(this.all_field_correct, this.Submit)
  }
}
