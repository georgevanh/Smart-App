import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Customer } from '../models/customer.model';
import { environment } from 'src/environments/environment';



@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  private apiUrl = environment.apiUrl; 
  constructor(private http: HttpClient) { }

  // Get all customers
  getCustomers(): Observable<Customer[]> {
    const headers = new HttpHeaders().set('Api-Key', environment.apiKey);
    return this.http.get<Customer[]>(this.apiUrl, { headers });
  }

  // Get customer by ID
  getCustomerById(id: number): Observable<Customer> {
    const url = `${this.apiUrl}/${id}`;
    const headers = new HttpHeaders().set('Api-Key', environment.apiKey);
    return this.http.get<Customer>(url, { headers });
  }

  // Add a new customer
  addCustomer(customer: Customer): Observable<Customer> {
    const headers = new HttpHeaders().set('Api-Key', environment.apiKey);
    return this.http.post<Customer>(this.apiUrl, customer, { headers });
  }

  // Update an existing customer
  updateCustomer(customer: Customer): Observable<Customer> {
    const url = `${this.apiUrl}/${customer.id}`;
    const headers = new HttpHeaders().set('Api-Key', environment.apiKey);
    return this.http.put<Customer>(url, customer, { headers });
  }

  // Delete a customer by ID
  deleteCustomer(id: number): Observable<void> {
    const url = `${this.apiUrl}/${id}`;
    const headers = new HttpHeaders().set('Api-Key', environment.apiKey);
    return this.http.delete<void>(url, { headers });
  }
}
