import {Component, OnInit} from '@angular/core';
import {RouterLink} from "@angular/router";
import {DoctorModel} from "../../models/doctor.model";
import {HttpService} from "../../services/http.service";
import {CommonModule} from "@angular/common";

@Component({
  selector: 'app-doctors',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './doctors.component.html',
  styleUrl: './doctors.component.scss'
})
export class DoctorsComponent implements OnInit {
  doctors: DoctorModel[] = [];

  ngOnInit(): void {
    this.getAll();
  }

  constructor(private http: HttpService) {
  }

  getAll() {
    this.http.post<DoctorModel[]>("Doctor/GetAll", {}, (res) => {
      this.doctors = res.data;
    })
  }
}
