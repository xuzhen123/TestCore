Gq.define(['jquery'], function (exports) {
    "use strict";

    var $ = Gq.jquery,
        win = Gq.win,
        doc = win.document;

    var Creditcard = function () {
    };

    Creditcard.prototype.config = {
        supports: null,
        number: null,
        code: null
    };

    Creditcard.prototype.init = function (supports, number, code) {
        var that = this;
        that.config.supports = $(supports);
        that.config.number = $(number);
        that.config.code = $(code);

        that.config.number.bind('change', format);
        that.config.number.bind('keyup', format);

        function format() {
            var $supports = that.config.supports,
                $number = that.config.number,
                $code = that.config.code,
                vN = $number.val();

            if (/^4\d*/.test(vN)) { //Visa: 4
                $supports.find("img[alt='visa']").addClass('selected').show().siblings().removeClass("selected").hide();
                $number.attr("maxlength", "16");
                $code.attr("maxlength", "3");
            }
            else if (/^[25]\d*/.test(vN)) { //MasterCard: 5/222100-272099
                if (/^[2]\d*/.test(vN)) {
                    var bin = parseInt(vN.substring(0, 4), 10);
                    if (bin < 2221 || bin > 2720)
                        return;
                }
                $supports.find("img[alt='mastercard']").addClass('selected').show().siblings().removeClass("selected").hide();
                $number.attr("maxlength", "16");
                $code.attr("maxlength", "3");
            } else if (/^35\d*/.test(vN) && !/^35[019]\d*/.test(vN) && !/^352[01234567]\d*/.test(vN)) { //Jcb: 3528-3589
                $supports.find("img[alt='Jcb']").addClass('selected').show().siblings().removeClass("selected").hide();
                $number.attr("maxlength", "16");
                $code.attr("maxlength", "3");
            } else if (/^3[47]\d*/.test(vN)) { //American Express: 34, 37
                $supports.find("img[alt='americanexpress']").addClass('selected').show().siblings().removeClass("selected").hide();
                $number.attr("maxlength", "15");
                $code.attr("maxlength", "4");
            } else if (/^6011\d*/.test(vN) || (/^64\d*/.test(vN) && !/^64[0123]\d*/.test(vN)) || /^65\d*/.test(vN)) { //Discover: 6011, 644-649, 65
                $supports.find("img[alt='discover']").addClass('selected').show().siblings().removeClass("selected").hide();
                $number.attr("maxlength", "16");
                $code.attr("maxlength", "3");
            } else if ((/^30\d*/.test(vN) && !/^30[678]\d*/.test(vN)) || /^3[689]\d*/.test(vN)) { //Diners Club: 300-305, 309, 36, 38-39
                $supports.find("img[alt='dinersclub']").addClass('selected').show().siblings().removeClass("selected").hide();
                $number.attr("maxlength", "14");
                $code.attr("maxlength", "3");
            } else if (/^62\d*/.test(vN) || /^638888\d*/.test(vN) || /^685800\d*/.test(vN)) { //UnionPay: 62, 638888, 685800
                $supports.find("img[alt='unionpay']").addClass('selected').show().siblings().removeClass("selected").hide();
                $number.attr("maxlength", "19");
                $code.attr("maxlength", "3");
            } else if (vN.length > 1) {
                $supports.find("img").removeClass("selected").hide();
            } else {
                $supports.find("img").removeClass("selected").show();
            }
        }
    };

    exports('creditcard', new Creditcard());
});