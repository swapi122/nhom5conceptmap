
$(document).ready(function ()
{
    $(function () {
        var $gallery = $("#gallery"),
          $trash = $("#trash");

        $("li", $gallery).draggable({
            cancel: "a.ui-icon",
            revert: true,
            containment: "document",
            helper: "original",
            cursor: "move"
        });

        $trash.droppable({
            accept: "#gallery > li",
            drop: function (event, ui) {
                addItem(ui.draggable);
            }
        });

        $gallery.droppable({
            accept: "#trash li",
            //activeClass: "custom-state-active",
            drop: function (event, ui) {
                removeItem(ui.draggable);
            }
        });

        var remove_item_icon = "<a title='Remove item' class='ui-icon ui-icon-minus'>-</a>";
        function addItem($item) {
            $item.fadeOut(1, function () {
                var $list = $("ul", $trash).length ?
                  $("ul", $trash) :
                  $("<ul class='gallery ui-helper-reset'/>").appendTo($trash);
                $item.find("a.ui-icon-plus").remove();
                $item.append(remove_item_icon).appendTo($list).fadeIn(function () {
                    $item.draggable({ revert: false, containment: '#trash' });
                    //$item.css({ "width": "100px","border":"2px solid orange","border-radius":"5px","background-color":"orange","color":"white"})
                });
            });
        }
        var add_item_icon = "<a title='Add this Item' class='ui-icon ui-icon-plus'>+</a>";
        function removeItem($item) {
            $item.fadeOut(1, function () {
                $item
                  .find("a.ui-icon-minus")
                  .remove()
                  .end()
                   //.css({ "width": "140px", "height": "", "border": "1px dashed orange" })
                  .append(add_item_icon)
                  .appendTo($gallery)
                  .fadeIn();
            });
        }

        function restoreItem($item) {
            $item.draggable({
                cancel: "a.ui-icon",
                revert: true,
                containment: "document",
                helper: "original",
                cursor: "move",
            });
        }
        $("ul.gallery > li").click(function (event) {
            var $item = $(this),
              $target = $(event.target);
            if ($target.is("a.ui-icon-plus")) {
                addItem($item);
            } else if ($target.is("a.ui-icon-minus")) {
                removeItem($item);
            }
            return false;
        });
    });
});