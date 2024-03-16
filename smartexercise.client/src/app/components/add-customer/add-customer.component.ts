// add-customer.component.ts
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
  cancel(): void {
    this.router.navigate(['/']);
  }
}

