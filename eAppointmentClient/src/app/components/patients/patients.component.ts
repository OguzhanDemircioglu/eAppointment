import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {CommonModule} from "@angular/common";
import {RouterLink} from "@angular/router";
import {FormsModule, NgForm} from "@angular/forms";
import {FormValidateDirective} from "form-validate-angular";
import {PatientModel} from "../../models/patient.model";
import {HttpService} from "../../services/http.service";
import {SwalService} from "../../services/swal.service";
import {PatientPipe} from "../../pipes/patient.pipe";

@Component({
  selector: 'app-patients',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule, FormValidateDirective, PatientPipe],
  templateUrl: './patients.component.html',
  styleUrl: './patients.component.scss'
})
export class PatientsComponent implements OnInit {
  patients: PatientModel[] = [];
  createModel: PatientModel = new PatientModel();
  updateModel: PatientModel = new PatientModel();
  search: string = "";

  @ViewChild("addModalCloseBtn") addModalCloseButton: ElementRef<HTMLButtonElement> | undefined;
  @ViewChild("updateModelCloseBtn") updateModelCloseBtn: ElementRef<HTMLButtonElement> | undefined;

  ngOnInit(): void {
    this.getAll();
  }

  constructor(
    private http: HttpService,
    private swal: SwalService) {
  }

  getAll() {
    this.http.post<PatientModel[]>("Patient/GetAll", {}, (res) => {
      this.patients = res.data;
    })
  }

  add(form: NgForm) {
    if (form.valid) {
      this.http.post<string>("Patient/Create", this.createModel, (res) => {
        if (res) {
          this.swal.callToast(res.data);
          this.getAll();
          this.addModalCloseButton?.nativeElement.click();
          this.createModel = new PatientModel();
        }
      });
    }
  }

  delete(id: string) {
    this.swal.callSwalForDelete(() => {
      this.http.post<string>("Patient/Delete", {id: id}, (res) => {
        this.swal.callToast(res.data);
        this.getAll();
      });
    });
  }

  get(data: PatientModel) {
    this.updateModel = {...data};
  }

  update(form: NgForm) {
    if (form.valid) {
      this.http.post<string>("Patient/Update", this.updateModel, (res) => {
        if (res) {
          this.swal.callToast(res.data);
          this.getAll();
          this.updateModelCloseBtn?.nativeElement.click();
          this.updateModel = new PatientModel();
        }
      });
    }
  }
}
