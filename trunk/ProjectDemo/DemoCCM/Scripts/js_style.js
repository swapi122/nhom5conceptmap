
$(document).ready(function ()
{
    $(function () {
        
        var $trash = $("#trash");
        var $gallery = $("#gallery");
      /*$("li", $gallery).draggable({        
          revert: true,
          containment: "docment",
          helper: "original",
          cursor: "pointer"
        });
         
        $trash.droppable({
            accept: "#gallery > li",
            drop: function (event, ui) {
                addItem(ui.draggable);
            }
        });

        $gallery.droppable({
            accept: "#trash li",
            drop: function (event, ui) {
                removeItem(ui.draggable);
            }
        });*/
        var remove_item_icon = "<a title='Remove item' class='ui-icon ui-icon-minus'>-</a>";
        function addItem($item) {
            $item.draggable({
                cancel: "a.ui-icon",
                revert: false,
                containment: '#trash',
                helper: "original",
                cursor: "pointer"
            });
            $item.fadeOut(1, function () {                
                var $list = $("ul", $trash).length ?
                $("ul", $trash) :
                $("<ul class='gallery ui-helper-reset'/>").appendTo($trash);
                $item.find("a.ui-icon-plus").remove();
                $item.append(remove_item_icon).appendTo($list)
                .fadeIn();
            });
        }
        var add_item_icon = "<a title='Add this Item' class='ui-icon ui-icon-plus'>+</a>";
        function removeItem($item) {
            $item.draggable('disable');
            $item.fadeOut(1, function () {
                $item.css({ "top":'', "left": ''})
                  .find("a.ui-icon-minus")
                  .remove()
                  .end()      
                  .append(add_item_icon)
                  .appendTo($gallery)
                  .fadeIn();
            });
        }
     
        $("ul.gallery > li").click(function (event) {
            var $item = $(this),
              $target = $(event.target);
            $('.highlight_stay').removeClass('highlight_stay');
            $(this).addClass('highlight_stay');

            if ($target.is("a.ui-icon-plus")) {
                addItem($item);
                $item.draggable('enable');
            } else if ($target.is("a.ui-icon-minus")) {
                removeItem($item);
                
            }
            return false;
        });
    });
});