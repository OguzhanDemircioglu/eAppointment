import {Pipe, PipeTransform} from '@angular/core';
import {PatientModel} from "../models/patient.model";

@Pipe({
  name: 'patient',
  standalone: true
})
export class PatientPipe implements PipeTransform {

  transform(value: PatientModel[], search: string): PatientModel[] {
    if (!search) {
      return value;
    }

    return value.filter(p =>
      p.fullName.toLowerCase().includes(search.toLowerCase()) ||
      p.identityNumber.toLowerCase().includes(search.toLowerCase()) ||
      p.town.toLowerCase().includes(search.toLowerCase()) ||
      p.city.toLowerCase().includes(search.toLowerCase()) ||
      p.fullAddress.toLowerCase().includes(search.toLowerCase())
    );
  }
}
