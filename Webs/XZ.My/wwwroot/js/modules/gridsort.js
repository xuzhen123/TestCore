Gq.define(function (exports) {
    "use strict";

    //创建排序的构造函数
    var GridSort = function () {
    };

    //公共的静态函数
    GridSort._StringByConvert = function (val, valType) {
        if (!valType) return val.toString();
        switch (valType.toLowerCase()) {
            case 'int':
                return parseInt(val);
            case 'float':
                return parseFloat(val);
            case 'date':
                return new Date(Date.parse(val));
            default:
                return val.toString();
        }
    };

    //支持排序的核心方法
    GridSort.prototype._Sequence = function (colIdx, colType) {
        return (function () {
            var _rowPrev = arguments[0].cells[colIdx].firstChild,
                _rowAfter = arguments[1].cells[colIdx].firstChild;
            var _rowPrevType = _rowPrev.nodeName.toLowerCase(),
                _rowAfterType = _rowAfter.nodeName.toLowerCase();
            var _rowPrevVal = GridSort._StringByConvert(_rowPrevType === '#text' ? _rowPrev.nodeValue : _rowPrev.firstChild.nodeValue, colType),  //这个相当于A参数
                _rowAfterVal = GridSort._StringByConvert(_rowAfterType === '#text' ? _rowAfter.nodeValue : _rowAfter.firstChild.nodeValue, colType); //这个相当于B参数
            if (_rowPrevVal < _rowAfterVal)
                return -1;
            else if (_rowPrevVal > _rowAfterVal)
                return 1;
            else
                return 0;
        });
    };

    //Grid列头点击事件的处理方法
    GridSort.prototype.Bind = function (selector) {
        var _grid = document.querySelector(selector);
        if (_grid === null) return;
        var _ghead = _grid.tHead,
            _gbody = _grid.tBodies[0],
            _cellsHead = _ghead.rows[0].cells,
            _rowsBody = _gbody.rows,
            _girdSort = this._Sequence;

        for (var i = 0, count = _cellsHead.length; i < count; i++) {
            //注意这里，这里为了避免闭包的影响使用了匿名函数
            (function (idx) {
                if (_cellsHead[idx].className.indexOf('sort') === -1) return;
                _cellsHead[idx].onclick = function () {
                    var _sortRows = [],
                        _sortType = this.getAttribute('stype'),
                        _orderby = _gbody.getAttribute('gq-orderby');

                    for (var i = 0, count = _rowsBody.length; i < count; i++) {
                        _sortRows[i] = _rowsBody[i];
                    }

                    if (!_orderby) {
                        _sortRows.sort(_girdSort(idx, _sortType));
                        _gbody.setAttribute('gq-orderby', 'asc');
                    } else {
                        _sortRows.reverse();
                        _gbody.removeAttribute('gq-orderby');
                    }

                    var _newRows = document.createDocumentFragment();
                    for (var j = 0, count2 = _sortRows.length; j < count2; j++) {
                        _newRows.appendChild(_sortRows[j]);
                    }
                    _gbody.appendChild(_newRows);
                };
            })(i);
        }
    };

    var gridsort = new GridSort();
    gridsort.Bind('.grid');

    exports('gridsort', gridsort);
});