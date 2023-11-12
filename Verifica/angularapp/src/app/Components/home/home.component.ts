import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { HeaderMenus } from 'src/app/Models/header-menu.dto';
import { HeaderMenusService } from 'src/app/Services/header-menus.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent {
  showButtons: boolean = false;

 
  ngOnInit(): void {
    
  }
 
}
