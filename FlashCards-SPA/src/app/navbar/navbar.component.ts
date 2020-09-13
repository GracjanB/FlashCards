import {Component, OnInit} from '@angular/core';

@Component({
  selector: 'app-main-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit{
  isCollapsed: boolean;

  constructor() {}

  ngOnInit(): void {
    this.isCollapsed = false;
  }

}
