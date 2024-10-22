import {Pipe, PipeTransform} from '@angular/core';
import {DoctorModel} from "../models/doctor.model";

@Pipe({
  name: 'doctor',
  standalone: true
})
export class DoctorPipe implements PipeTransform {

  transform(value: DoctorModel[], search: string): DoctorModel[] {
    if (!search) {
      return value;
    }

    return value.filter(p =>
      p.fullName.toLowerCase().includes(search.toLowerCase()) ||
      p.department.name.toLowerCase().includes(search.toLowerCase())
    );
  }
}
