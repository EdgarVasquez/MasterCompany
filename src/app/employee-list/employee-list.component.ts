import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit {
  employees: any[] = []; // Declarando e inicializando la propiedad employees

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.fetchEmployees();
  }
  formatSalary(salary: number): string {
    return new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD' }).format(salary);
  }
  
  fetchEmployees() {
    this.http.get<any[]>('https://mastercompanyapi.azurewebsites.net/api/Employee')
      .subscribe(
        (data) => {
          this.employees = data;
        },
        (error) => {
          console.error('Error fetching employees:', error);
        }
      );
  }
}
