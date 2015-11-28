function S4() {
    return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
}

function MakeRequest() {
    if ((sesReqUrl != null)) {
        var Ans = (S4() + S4() + "-" + S4() + "-" + S4() + "-" + S4() + "-" + S4() + S4() + S4()).toUpperCase();
        $.get(sesReqUrl + "?id=" + Ans, function () {
            Delay();
        });
    }
}

function Delay() {
    if (sesReqTime != null) {
        setTimeout(MakeRequest, sesReqTime * 1000);
    }
}

MakeRequest();