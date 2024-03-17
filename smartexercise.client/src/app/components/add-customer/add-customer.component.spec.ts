import { AddCustomerComponent } from './add-customer.component';
import { Router } from '@angular/router';
import { CustomerService } from '../../services/customer.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { of } from 'rxjs';

describe('AddCustomerComponent', () => {
  let component: AddCustomerComponent;
  let routerSpy: jasmine.SpyObj<Router>;
  let customerServiceSpy: jasmine.SpyObj<CustomerService>;
  let snackBarSpy: jasmine.SpyObj<MatSnackBar>;

  beforeEach(() => {
    routerSpy = jasmine.createSpyObj('Router', ['navigate']);
    customerServiceSpy = jasmine.createSpyObj('CustomerService', ['addCustomer']);
    snackBarSpy = jasmine.createSpyObj('MatSnackBar', ['open']);

    component = new AddCustomerComponent(routerSpy, customerServiceSpy, snackBarSpy);
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize component', () => {
    expect(component.firstName).toEqual('');
    expect(component.lastName).toEqual('');
    expect(component.email).toEqual('');
    expect(component.mobileNumber).toEqual('');
    expect(component.address).toEqual('');
  });

  it('should navigate to home when cancel is called', () => {
    component.cancel();
    expect(routerSpy.navigate).toHaveBeenCalledWith(['/']);
  });

  it('should return false and show error message if any field is empty', () => {
    component.firstName = 'John';
    component.lastName = '';
    component.email = 'john@example.com';
    component.mobileNumber = '0423456789';
    component.address = '123 Street';

    const result = component.validateForm();
    expect(result).toBeFalse();
    expect(snackBarSpy.open).toHaveBeenCalledWith('Please fill in all fields', 'Close', jasmine.any(Object));
  });

  it('should return false and show error message for invalid email format', () => {
    component.firstName = 'John';
    component.lastName = 'Doe';
    component.email = 'john@example'; // Invalid email format
    component.mobileNumber = '0423456789';
    component.address = '123 Street';

    const result = component.validateForm();
    expect(result).toBeFalse();
    expect(snackBarSpy.open).toHaveBeenCalledWith('Invalid email format', 'Close', jasmine.any(Object));
  });

  it('should return false and show error message for invalid mobile number format', () => {
    component.firstName = 'John';
    component.lastName = 'Doe';
    component.email = 'john@example.com';
    component.mobileNumber = '123456789'; // Invalid mobile number format
    component.address = '123 Street';

    const result = component.validateForm();
    expect(result).toBeFalse();
    expect(snackBarSpy.open).toHaveBeenCalledWith('Invalid Australian mobile number format', 'Close', jasmine.any(Object));
  });

  it('should return true if form is valid', () => {
    component.firstName = 'John';
    component.lastName = 'Doe';
    component.email = 'john@example.com';
    component.mobileNumber = '0423456789';
    component.address = '123 Street';

    const result = component.validateForm();
    expect(result).toBeTrue();
    expect(snackBarSpy.open).not.toHaveBeenCalled();
  });

  it('should call customerService.addCustomer and show success message on successful submission', () => {
    const newCustomer = {
      firstName: 'John',
      lastName: 'Doe',
      email: 'john@example.com',
      mobileNumber: '0423456789',
      address: '123 Street'
    };

    customerServiceSpy.addCustomer.and.returnValue(of(newCustomer));

    component.firstName = 'John';
    component.lastName = 'Doe';
    component.email = 'john@example.com';
    component.mobileNumber = '0423456789';
    component.address = '123 Street';

    component.onSubmit();
    expect(customerServiceSpy.addCustomer).toHaveBeenCalledWith(newCustomer);
    expect(snackBarSpy.open).toHaveBeenCalledWith('Customer added successfully', 'Close', jasmine.any(Object));
  });

  it('should call snackBar.open and log error on failed submission', () => {
    const error = 'Some error occurred';

    customerServiceSpy.addCustomer.and.throwError(error);

    component.firstName = 'John';
    component.lastName = 'Doe';
    component.email = 'john@example.com';
    component.mobileNumber = '0423456789';
    component.address = '123 Street';

    spyOn(console, 'error');

    component.onSubmit();
    expect(customerServiceSpy.addCustomer).toHaveBeenCalled();
    expect(snackBarSpy.open).toHaveBeenCalledWith('Error adding customer, please check!', 'Close', jasmine.any(Object));
    expect(console.error).toHaveBeenCalledWith('Error adding customer:', error);
  });
});
