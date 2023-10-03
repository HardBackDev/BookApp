import { Component, OnInit, Renderer2 } from '@angular/core';
import { AuthenticationService } from './services/authentication.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  _authService: AuthenticationService

  constructor(private authService: AuthenticationService, private renderer: Renderer2){
    this._authService = authService
  }
  
  ngOnInit(){

    const bodyElement = document.body;
    
    this.renderer.setStyle(bodyElement, 'overflow-x', 'hidden');
  }

  logOut = () => {
    localStorage.removeItem("jwt");
  }
}
