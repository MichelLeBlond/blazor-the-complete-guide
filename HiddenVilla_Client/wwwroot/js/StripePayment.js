redirectToCheckout = function (sessionId) {
    var stripe = Stripe('pk_test_51JfBK9HUS4rvLFr7vpDOD9tvM1BRtEo8nqv04XyoZIn4fAog78N1r1MmOfe4UANqb4fYXMwUOz9ZrrQo6QqZZTc500e2u5dBqW');
    stripe.redirectToCheckout({
        sessionId: sessionId
    });
};