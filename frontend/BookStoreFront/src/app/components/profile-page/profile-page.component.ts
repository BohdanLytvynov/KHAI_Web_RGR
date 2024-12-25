import { Component, Inject, OnChanges, OnDestroy, OnInit, SimpleChanges } from '@angular/core';
import { RouterLink } from '@angular/router';
import { DataService } from '../../services/data-service/data.service';
import { DataExchangeService } from '../../services/data-exchange/data-exchange.service';
import { Subject, Subscription } from 'rxjs';
import { currentUser } from '../../data/mockData';
import { User } from '../../implementations/User/User';

@Component({
  selector: 'app-profile-page',
  standalone: true,
  imports: [RouterLink],
  providers: [DataService],
  templateUrl: './profile-page.component.html',
  styleUrl: './profile-page.component.css'
})
export class ProfilePageComponent implements OnInit{ 
  
  curentUserNickname: string = '';
  currentUser: User = {
    nickname: '',
    password: '',
    name: '',
    surename: '',
    birthday: '',
    address: ''
  };

  constructor(@Inject(DataExchangeService) private dataExchangeService: DataExchangeService) 
  {
    
  }
  

  ngOnInit(): void {    
      this.currentUser = this.dataExchangeService.getUser();
  }


  
        
}
