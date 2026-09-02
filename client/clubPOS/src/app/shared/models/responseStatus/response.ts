export class BaseResponse<T> {
    public message?: string;
    public status: ResponseEnum;
    public data: T | null;
  
    constructor(status: ResponseEnum, data: T | null, message?: string) {
      this.status = status;
      this.message = message;
      this.data = data;
    }
  }
  
  // Enum for response statuses
  export enum ResponseEnum {
    Success,
    Error,
    Failed
  }
  
  // Function to map API responses to BaseResponse
  export function mapApiResponse<T>(response: any): BaseResponse<T | null> {
    const statusEnum = ResponseEnum[response.status as keyof typeof ResponseEnum];
    return new BaseResponse<T>(statusEnum, response.data ? response.data : null, response.message);
  }
  