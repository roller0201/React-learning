export class BaseResponse {
  public success: boolean;
  public messages: string[]

  constructor(success?: boolean, messages?: string[]) {
    this.success = success;
    this.messages = messages;
  }
}