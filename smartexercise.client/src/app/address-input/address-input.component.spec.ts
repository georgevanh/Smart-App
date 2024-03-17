import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { Subject } from 'rxjs';
import { AddressInputComponent } from './address-input.component';

describe('AddressInputComponent', () => {
  let component: AddressInputComponent;
  let fixture: ComponentFixture<AddressInputComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddressInputComponent],
      imports: [
        BrowserAnimationsModule,
        FormsModule,
        ReactiveFormsModule,
        MatFormFieldModule,
        MatInputModule
      ]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddressInputComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize properly', () => {
    expect(component.placeholder).toEqual('');
    expect(component.disabled).toBeFalse();
    expect(component.stateChanges).toBeTruthy();
    expect(component.focused).toBeFalse();
    expect(component.controlType).toEqual('app-address-input');
    expect(component.id).toContain('address-input-');
    expect(component.describedBy).toEqual('');
    expect(component.value).toBeNull();
    expect(component.empty).toBeTrue();
    expect(component.shouldLabelFloat).toBeFalse();
    expect(component.errorState).toBeFalse();
    expect(component.required).toBeFalse();
  });

  it('should handle input', () => {
    const event = {
      target: {
        value: '123 Test St'
      }
    } as Event;
    component.handleInput(event);
    expect(component.value).toEqual('123 Test St');
    expect(component.onTouched).toHaveBeenCalled();
  });

  it('should handle container click', () => {
    spyOn(component['_elementRef'].nativeElement, 'querySelector').and.returnValue({
      focus: jasmine.createSpy()
    });
    const event = new MouseEvent('click');
    component.onContainerClick(event);
    expect(component['_elementRef'].nativeElement.querySelector).toHaveBeenCalledWith('input');
  });

  it('should register on change and on touch', () => {
    const fn = jasmine.createSpy();
    component.registerOnChange(fn);
    component.registerOnTouched(fn);
    expect(component.onChange).toEqual(fn);
    expect(component.onTouched).toEqual(fn);
  });

  it('should set described by ids', () => {
    component.setDescribedByIds(['id1', 'id2']);
    expect(component.describedBy).toEqual('id1 id2');
  });

  it('should set value', () => {
    component.writeValue('123 Test St');
    expect(component.value).toEqual('123 Test St');
    expect(component.stateChanges.next).toHaveBeenCalled();
  });

  it('should set disabled state', () => {
    component.setDisabledState(true);
    expect(component.disabled).toBeTrue();
    expect(component.stateChanges.next).toHaveBeenCalled();
  });
});
