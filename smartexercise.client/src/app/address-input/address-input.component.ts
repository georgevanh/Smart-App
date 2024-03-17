import { Component, ElementRef, Input, OnInit, OnDestroy, Optional, Self, NgZone, ViewChild, AfterViewInit } from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';
import { MatFormFieldControl } from '@angular/material/form-field';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-address-input',
  templateUrl: './address-input.component.html',
  styleUrls: ['./address-input.component.css'],
  providers: [
    { provide: MatFormFieldControl, useExisting: AddressInputComponent }
  ]
})
export class AddressInputComponent implements MatFormFieldControl<string>, ControlValueAccessor, OnInit, OnDestroy, AfterViewInit {
  static nextId = 0;
  @ViewChild('addressInput') addressInputRef!: ElementRef;
  @Input() placeholder: string = '';
  @Input() disabled: boolean = false;

  stateChanges = new Subject<void>();
  focused = false;
  controlType = 'app-address-input';
  id = `address-input-${AddressInputComponent.nextId++}`;
  describedBy = '';

  private _value: string | null = '';

  constructor(private zone: NgZone,
    @Optional() @Self() public ngControl: NgControl,
    private _elementRef: ElementRef<HTMLElement>
  ) {
    if (this.ngControl != null) {
      this.ngControl.valueAccessor = this;
    }
  }

  ngOnInit() {
    
    if (this.ngControl != null) {
      this.ngControl.valueAccessor = this;
    }
  }

  ngAfterViewInit() {
    this.initAutocomplete();
  }

  private initAutocomplete(): void {
    const autocomplete = new google.maps.places.Autocomplete(this.addressInputRef.nativeElement);
    autocomplete.addListener('place_changed', () => {
      this.zone.run(() => {
        const place = autocomplete.getPlace();
        // Handle selected place
      });
    });
  }

  ngOnDestroy() {
    this.stateChanges.complete();
  }

  writeValue(value: string | null): void {
    this.value = value;
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  setDisabledState(isDisabled: boolean): void {
    this.disabled = isDisabled;
    this.stateChanges.next();
  }

  get value(): string | null {
    return this._value;
  }

  set value(value: string | null) {
    this._value = value;
    this.onChange(value); // Notify Angular about value changes
    this.stateChanges.next();
  }

  get empty(): boolean {
    return !this.value;
  }

  get shouldLabelFloat(): boolean {
    return this.focused || !this.empty;
  }

  onChange: any = () => { };
  onTouched: any = () => { };

  get errorState(): boolean {
    return !!this.ngControl?.errors && (this.ngControl?.touched ?? false);
  }

  get required(): boolean {
    return !!(
      this.ngControl &&
      this.ngControl.control &&
      this.ngControl.control.validator &&
      this.ngControl.control.validator({} as any)?.['required']
    );
  }

  onContainerClick(event: MouseEvent): void {
    const inputElement = this._elementRef.nativeElement.querySelector('input');
    if (inputElement) {
      inputElement.focus();
    }
  }

  setDescribedByIds(ids: string[]): void {
    this.describedBy = ids.join(' ');
  }

  handleInput(event: Event): void {
    const inputElement = event.target as HTMLInputElement;
    this.value = inputElement.value;
    this.onTouched(); // Notify Angular that the input was touched
  }
}
