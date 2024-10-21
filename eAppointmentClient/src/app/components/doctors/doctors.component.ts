import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {RouterLink} from "@angular/router";
import {DoctorModel} from "../../models/doctor.model";
import {HttpService} from "../../services/http.service";
import {CommonModule} from "@angular/common";
import {FormsModule, NgForm} from "@angular/forms";
import {departments} from "../../constats";
import {FormValidateDirective} from "form-validate-angular";

@Component({
  selector: 'app-doctors',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule, FormValidateDirective],
  templateUrl: './doctors.component.html',
  styleUrl: './doctors.component.scss'
})
export class DoctorsComponent implements OnInit {
  doctors: DoctorModel[] = [];
  createModel: DoctorModel = new DoctorModel();
  updateModel: DoctorModel = new DoctorModel();
  departments = departments;

  @ViewChild("addModalCloseBtn") addModalCloseButton: ElementRef<HTMLButtonElement> | undefined;

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

  add(form: NgForm) {
    if (this.createModel.departmentValue === 0) {
      alert("Select Department!");
      return;
    }
    if (form.valid) {
      this.http.post<string>("Doctor/Create", this.createModel, (res) => {
        if (res) {
          alert(res.data);
          this.getAll();
          this.addModalCloseButton?.nativeElement.click();
          this.createModel = new DoctorModel();
        }
      });
    }
  }
}
