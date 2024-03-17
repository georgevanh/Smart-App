import { TestBed, inject } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { CustomerService } from './customer.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Customer } from '../models/customer.model';

describe('CustomerService', () => {
  let service: CustomerService;
  let httpTestingController: HttpTestingController;
  let httpClient: HttpClient;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [CustomerService]
    });

    service = TestBed.inject(CustomerService);
    httpTestingController = TestBed.inject(HttpTestingController);
    httpClient = TestBed.inject(HttpClient);
  });

  afterEach(() => {
    httpTestingController.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should fetch customers from API via GET', () => {
    const mockCustomers: Customer[] = [
      { id: 1, firstName: 'John', lastName: 'Doe', email: 'john.doe@example.com', mobileNumber: '1234567890', address: '123 Street' },
      { id: 2, firstName: 'Jane', lastName: 'Smith', email: 'jane.smith@example.com', mobileNumber: '0987654321', address: '456 Avenue' }
    ];

    service.getCustomers().subscribe(customers => {
      expect(customers).toEqual(mockCustomers);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}`);
    expect(req.request.method).toEqual('GET');
    req.flush(mockCustomers);
  });

  it('should fetch customer by ID from API via GET', () => {
    const mockCustomer: Customer = {
      id: 1,
      firstName: 'John',
      lastName: 'Doe',
      email: 'john.doe@example.com',
      mobileNumber: '1234567890',
      address: '123 Street'
    };

    service.getCustomerById(1).subscribe(customer => {
      expect(customer).toEqual(mockCustomer);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/1`);
    expect(req.request.method).toEqual('GET');
    req.flush(mockCustomer);
  });

  it('should add a new customer via POST', () => {
    const newCustomer: Customer = {
      firstName: 'New',
      lastName: 'Customer',
      email: 'new.customer@example.com',
      mobileNumber: '9876543210',
      address: '789 Boulevard'
    };

    service.addCustomer(newCustomer).subscribe(customer => {
      expect(customer).toEqual(newCustomer);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}`);
    expect(req.request.method).toEqual('POST');
    req.flush(newCustomer);
  });

  it('should update an existing customer via PUT', () => {
    const updatedCustomer: Customer = {
      id: 1,
      firstName: 'Updated',
      lastName: 'Customer',
      email: 'updated.customer@example.com',
      mobileNumber: '9876543210',
      address: '789 Boulevard'
    };

    service.updateCustomer(updatedCustomer).subscribe(customer => {
      expect(customer).toEqual(updatedCustomer);
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/1`);
    expect(req.request.method).toEqual('PUT');
    req.flush(updatedCustomer);
  });

  it('should delete a customer by ID via DELETE', () => {
    service.deleteCustomer(1).subscribe(() => {
      expect().nothing();
    });

    const req = httpTestingController.expectOne(`${environment.apiUrl}/1`);
    expect(req.request.method).toEqual('DELETE');
    req.flush({});
  });
});
