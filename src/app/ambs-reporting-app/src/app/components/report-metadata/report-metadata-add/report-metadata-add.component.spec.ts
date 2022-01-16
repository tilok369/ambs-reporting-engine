import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportMetadataAddComponent } from './report-metadata-add.component';

describe('ReportMetadataAddComponent', () => {
  let component: ReportMetadataAddComponent;
  let fixture: ComponentFixture<ReportMetadataAddComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReportMetadataAddComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ReportMetadataAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
