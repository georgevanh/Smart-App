import { Component, OnInit } from '@angular/core';
import { CustomerService } from '../../services/customer.service';
import { Customer } from '../../models/customer.model';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';


@Component({
  selector: 'app-add-customer',
  templateUrl: './add-customer.component.html',
  styleUrls: ['./add-customer.component.css']
})
export class AddCustomerComponent implements OnInit {
  firstName: string = '';
  lastName: string = '';
  email: string = '';
  mobileNumber: string = '';
  address: string = '';

  constructor(private router: Router, private customerService: CustomerService, private snackBar: MatSnackBar) { }

  ngOnInit(): void { }

  onSubmit(): void {
    if (!this.validateForm()) {
      return; // Halt the script if validation fails
    }
    

    const newCustomer: Customer = {
      firstName: this.firstName,
      lastName: this.lastName,
      email: this.email,
      mobileNumber: this.mobileNumber,
      address: this.address
    };
    this.customerService.addCustomer(newCustomer)
      .subscribe(
        () => {

          // Show success message using MatSnackBar
          this.snackBar.open('Customer added successfully', 'Close', {
            duration: 3000, // Duration of the toaster message
            panelClass: ['custom-snackbar'] // CSS class for styling
          });
        },
        (error) => {

          this.snackBar.open('Error adding customer, please check!', 'Close', {
            duration: 3000, // Duration of the toaster message
            panelClass: ['success-toast'] // CSS class for styling
          });
          console.error('Error adding customer:', error);
          // Optionally, handle the error, display a message, or perform other actions
        }
      );
  }
  validateForm(): boolean {
    // Check if all fields are filled
    if (!this.firstName || !this.lastName || !this.email || !this.mobileNumber || !this.address) {
      this.snackBar.open('Please fill in all fields', 'Close', {
        duration: 3000,
        panelClass: ['error-toast']
      });
      return false;
    }

    // Validate email format
    const emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    if (!emailPattern.test(this.email)) {
      this.snackBar.open('Invalid email format', 'Close', {
        duration: 3000,
        panelClass: ['error-toast']
      });
      return false;
    }

    // Validate mobile number format
    const mobilePattern = /^04\d{8}$/;
    if (!mobilePattern.test(this.mobileNumber)) {
      this.snackBar.open('Invalid Australian mobile number format', 'Close', {
        duration: 3000,
        panelClass: ['error-toast']
      });
      return false;
    }
    // If all validations pass, return true
    return true;
  }
  cancel(): void {
    this.router.navigate(['/']);
  }
}

