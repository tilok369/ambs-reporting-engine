import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportMetadataEditComponent } from './report-metadata-edit.component';

describe('ReportMetadataEditComponent', () => {
  let component: ReportMetadataEditComponent;
  let fixture: ComponentFixture<ReportMetadataEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReportMetadataEditComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ReportMetadataEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
