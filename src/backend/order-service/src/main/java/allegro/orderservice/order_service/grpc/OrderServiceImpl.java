package allegro.orderservice.order_service.grpc;

import com.google.protobuf.Empty;

import allegro.orderservice.order_service.grpc.OrderServiceGrpc.OrderServiceImplBase;
import allegro.orderservice.order_service.grpc.OrderServiceOuterClass.ServiceResult;
import io.grpc.stub.StreamObserver;
import net.devh.boot.grpc.server.service.GrpcService;

@GrpcService
public class OrderServiceImpl extends OrderServiceImplBase {

    @Override
    public void testingGrpc(Empty request, StreamObserver<ServiceResult> responseObserver) {
        var result = ServiceResult.newBuilder().setSuccess(true).setMessage("You hit the grpc endpoint!").build();

        responseObserver.onNext(result);
        responseObserver.onCompleted();
    }

}
