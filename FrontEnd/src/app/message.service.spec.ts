import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule} from '@angular/common/http/testing';
import { MessageService } from './message.service';

describe('MessageService', () => {
  let service: MessageService;

  beforeEach(() => {
    TestBed.configureTestingModule({
        imports: [HttpClientTestingModule]
    });
    service = TestBed.inject(MessageService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should call getMessage()', () => {
    service.getMessage(1);
    expect(service).toBeTruthy();
  });

  it('should be getAllMessages()', () => {
    service.getAllMessages();
    expect(service).toBeTruthy();
  });

  it('should call sendMessage()', () => {
    let message: any;
    service.sendMessage(message);
    expect(service).toBeTruthy();
  });
});