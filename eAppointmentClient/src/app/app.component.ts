import {Component} from '@angular/core';
import {RouterOutlet} from '@angular/router';
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap/dist/css/bootstrap.css";
import "bootstrap/dist/js/bootstrap.js";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet],
  template: "<router-outlet></router-outlet>"
})
export class AppComponent {
}
