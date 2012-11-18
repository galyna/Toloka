/// <reference path="Scripts/jquery-1.4.1-vsdoc.js" />
$(function () {


    function FrontSlider() {
        "use strict";

        var FrontSlider = function () {
            var self = this;
            self.slidersImgs = [];
            self.slidersImgSources = [];
            self.Index = 0;
            self.ImgCount = 5;
            self.sliderItems = [];
            self.corners = [];
            self.animationDuration = 400;
            self.itemWidth = 280;
            self.itemDX = self.itemWidth - 20;
            self.itemDY = 10;
            self.itemMoveStep = self.itemWidth - 20;
            self.itemHeight = 180;
            self.itemCornerHeight = 10;

            self.leftCornerImg = "images/left-corner.png";
            self.rightCornerImg = "images/right-corner.png";
            self.itemCornerWidth = 20;
            var directions = ["prev", "next"];
            self.lastDirection = directions[0];
            self.centralShopsBarHeight = 35;

            self.bodyPaper = {};
            self.bodyDomElementId = "";
            self.bodyPaperWidth = 1400;
            self.bodyPaperHeight = 500;



            self.prevButtonImg = "images/prev-btn.png";
            self.nextButtonImg = "images/next-btn.png";



            var sliderItem = function (source, layoutSettings, order) {
                this.name = source.name;
                this.image = source.image;
                this.price = source.price;
                this.category = source.category;
                this.shops = source.shops;
                this.order = order;
                this.layout = layoutSettings;
                //flags
                this.isCenter = false;
                this.isPrev = false;
                this.isNext = false;
                this.isDisebled = false;
                this.hasCorner = true;
            };
            var layoutSettings = function (dx, dy, cornerImgUrl, cornerDx) {
                this.CornerImgUrl = cornerImgUrl;
                this.CornerDx = cornerDx;

                this.dx = dx;
                this.dy = dy;
                this.width = self.itemWidth;
                this.height = self.itemHeight;
            };

            //#region init adnd create Sliders
            self.init = function (slidersImgSources, domElement) {
                /// <summary>Initialize start elements:
                ///   <para>self.bodyPaper : slider container</para>
                ///   <para>assigns 5 first source objects of type FrontSlider.prototype.sliderSourceItem</para>
                ///   <para>creates 5 first slids </para>
                ///   <para>creates and binds nextButton, prevButton</para>
                /// </summary>
                /// <param name="slidersImgSources" type="object">collection of  FrontSlider.prototype.sliderSourceItem</param>
                /// <param name="domElement" type="String">id of DOM element(<div/>) for self.bodyPaper as slider container</param>
                self.bodyDomElementId = domElement;
                self.bodyPaper = Raphael(domElement, self.bodyPaperWidth, self.bodyPaperHeight);
                self.slidersImgSources = slidersImgSources;
                var sources = self.getStartSources();
                self.createSlide(sources);
                self.initPrevNextButtons();
            },
            self.initPrevNextButtons = function () {
                /// <summary>Initialize Prev and Next Buttons:
                ///   <para>binds onclick event</para>
                /// </summary>

                var prevBtn = self.bodyPaper.image(self.prevButtonImg, self.itemDX * 2 - 30, self.itemDY * 2 + self.itemHeight - 10, 50, 30);
                var nextBtn = self.bodyPaper.image(self.nextButtonImg, self.itemDX * 2 + self.itemWidth - 20, self.itemDY * 2 + self.itemHeight - 10, 50, 30);

                for (var y = 0; y < self.slidersImgs.length; y++) {

                    if (self.slidersImgs[y].sliderItem.isPrev) {
                        prevBtn[0].elenment = self.slidersImgs[y];
                        prevBtn[0].onclick = clickNextHandler;
                    }

                    if (self.slidersImgs[y].sliderItem.isNext) {
                        nextBtn[0].elenment = self.slidersImgs[y];
                        nextBtn[0].onclick = clickPrevHandler;
                    }
                }

            },
            ///#region Slides creation

            self.createSlide = function (sources) {
                /// <summary>Creates slide based on sources collection of  FrontSlider.prototype.sliderSourceItem:
                ///   <para>binds prev next onclick handlers </para>
                /// </summary>
                /// <param name="sources" type="object">collection of  FrontSlider.prototype.sliderSourceItem</param>
                self.sliderItems = self.initSequence(sources);
                for (var i = 0; i < self.sliderItems.length; i++) {
                    self.slidersImgs.push(self.initSliderImg(self.sliderItems[i]));
                }
                for (var y = 0; y < self.slidersImgs.length; y++) {
                    self.initSliderClick(self.slidersImgs[y]);
                    if (self.slidersImgs[y].sliderItem.isCenter) {
                        self.slidersImgs[y].toFront();
                    }
                }
            },
            ////#region init slid item from sequence
            self.initSequence = function (sources) {

                var sliderItems = [];
                var firstSlidelayout = new layoutSettings(0, 0, self.leftCornerImg, self.itemDX);
                var firstSlide = new sliderItem(sources[0], firstSlidelayout, 0);
                firstSlide.isDisebled = true;
                sliderItems.push(firstSlide);

                var secndSlidelayout = new layoutSettings(self.itemDX, self.itemDY, self.leftCornerImg, self.itemDX);
                var secndSlide = new sliderItem(sources[1], secndSlidelayout, 1);
                secndSlide.isPrev = true;
                sliderItems.push(secndSlide);

                var centralSlidelayout = new layoutSettings(self.itemDX * 2, self.itemDY * 2, "", 0);
                var centralSlide = new sliderItem(sources[2], centralSlidelayout, 2);
                centralSlide.isCenter = true;
                centralSlide.hasCorner = false;
                sliderItems.push(centralSlide);

                var thidSlidelayout = new layoutSettings(self.itemDX * 3, self.itemDY, self.rightCornerImg, 0);
                var thidSlide = new sliderItem(sources[3], thidSlidelayout, 3);
                thidSlide.isNext = true;
                sliderItems.push(thidSlide);

                var fiveSlidelayout = new layoutSettings(self.itemDX * 4, 0, self.rightCornerImg, 0);
                var fiveSlide = new sliderItem(sources[4], fiveSlidelayout, 4);
                fiveSlide.isDisebled = true;
                sliderItems.push(fiveSlide);

                return sliderItems;
            };
            self.updateSequence = function (source, index) {
                //clean flags
                source.isDisebled = false;
                source.isPrev = false;
                source.hasCorner = true;
                source.isNext = false;
                source.isCenter = false;

                switch (index) {
                    case 0:
                        var firstSlidelayout = new layoutSettings(0, 0, self.leftCornerImg, self.itemDX);
                        var firstSlide = new sliderItem(source, firstSlidelayout, 0);
                        firstSlide.isDisebled = true;
                        return firstSlide;
                    case 1:
                        var secndSlidelayout = new layoutSettings(self.itemDX, self.itemDY, self.leftCornerImg, self.itemDX);
                        var secndSlide = new sliderItem(source, secndSlidelayout, 1);
                        secndSlide.isPrev = true;
                        return secndSlide;
                    case 2:
                        var centralSlidelayout = new layoutSettings(self.itemDX * 2, self.itemDY * 2, "", 0);
                        var centralSlide = new sliderItem(source, centralSlidelayout, 2);
                        centralSlide.isCenter = true;
                        centralSlide.hasCorner = false;
                        return centralSlide;
                    case 3:
                        var thidSlidelayout = new layoutSettings(self.itemDX * 3, self.itemDY, self.rightCornerImg, 0);
                        var thidSlide = new sliderItem(source, thidSlidelayout, 3);
                        thidSlide.isNext = true;
                        return thidSlide;
                    case 4:
                        var fiveSlidelayout = new layoutSettings(self.itemDX * 4, 0, self.rightCornerImg, 0);
                        var fiveSlide = new sliderItem(source, fiveSlidelayout, 4);
                        fiveSlide.isDisebled = true;
                        return fiveSlide;

                    default:
                }

            },
            self.initFirst = function (source) {
                var firstSlidelayout = new layoutSettings(-self.itemDX, 0, self.leftCornerImg, self.itemDX);
                var firstSlide = new sliderItem(source, firstSlidelayout, -1);
                firstSlide.isDisebled = true;
                firstSlide.hasCorner = false;
                return firstSlide;
            };
            self.initLast = function (source) {
                var fiveSlidelayout = new layoutSettings(self.itemDX * 5, 0, self.rightCornerImg, 0);
                var fiveSlide = new sliderItem(source, fiveSlidelayout, 5);
                fiveSlide.isDisebled = true;
                fiveSlide.hasCorner = false;
                return fiveSlide;
            };

            //endregion
            ////#region indexing
            self.getStartSources = function () {
                var indexStart = self.Index;
                var sources = [];
                for (var i = indexStart, max = indexStart + self.ImgCount; i < max; i++) {
                    indexStart = i;
                    if (indexStart > self.slidersImgSources.length) {
                        max = self.ImgCount - images.length;
                        indexStart = 0;
                    }
                    if (self.slidersImgSources[i]) {
                        sources.push(self.slidersImgSources[i]);
                    }
                }
                self.Index = indexStart;
                return sources;
            },
            self.getSourceByIndex = function (index) {
                var source = {};
                for (var i = 0, max = self.slidersImgSources.length; i < max; i++) {
                    if (self.slidersImgSources[index]) {
                        source = self.slidersImgSources[index];
                    }
                }
                return source;
            },
            self.getNextSlideItem = function (index) {
                var nextIndex = index;
                var slideItem = {};

                if (nextIndex < self.slidersImgSources.length && nextIndex >= 0) {
                    slideItem = self.getSourceByIndex(nextIndex);
                    self.Index = nextIndex;
                    return slideItem;
                }

                if (Math.abs(nextIndex) >= self.slidersImgSources.length) {
                    self.Index = 0;
                    slideItem = self.getSourceByIndex(self.Index);
                    return slideItem;
                }

                if (nextIndex < 0 && Math.abs(nextIndex) < self.slidersImgSources.length) {
                    var newIndex = self.slidersImgSources.length + nextIndex;
                    slideItem = self.getSourceByIndex(newIndex);
                    self.Index = nextIndex;
                    return slideItem;
                }
            };
            //endregion

            //endregion
            //#region Prev
            var clickPrevHandler = function () {
                var sliderCanvas = this.elenment;
                var anim = Raphael.animation({ transform: "T 140, 10" }, 350);
                self.removeCorners();
                // prevent blinking
                self.updeteSlidersBeforePrev(sliderCanvas, anim);
                for (var p = 0; p < self.slidersImgs.length; p++) {

                    switch (self.slidersImgs[p].sliderItem.order) {
                        case 0:
                            self.slidersImgs[p].animateWith(sliderCanvas, anim, { opacity: " 1" }, self.animationDuration, ">");
                            self.slidersImgs[p].animateWith(sliderCanvas, anim, { transform: "T " + self.itemDX + ", " + self.itemDY }, self.animationDuration, ">");
                            self.bodyPaper.safari();
                            break;
                        case 1:
                            self.slidersImgs[p].animateWith(sliderCanvas, anim, { transform: "T " + self.itemDX + ", " + self.itemDY }, self.animationDuration, ">");
                            self.slidersImgs[p].toFront();
                            self.bodyPaper.safari();
                            break;
                        case 2:
                            self.slidersImgs[p].animateWith(sliderCanvas, anim, { transform: "T " + self.itemDX + ", -" + self.itemDY }, self.animationDuration, ">");
                            self.bodyPaper.safari();
                            break;
                        case 3:
                            self.slidersImgs[p].animateWith(sliderCanvas, anim, { opacity: " 0.8" }, self.animationDuration, ">");
                            self.slidersImgs[p].animateWith(sliderCanvas, anim, { transform: "T " + self.itemDX + ", -" + self.itemDY }, self.animationDuration, ">");
                            self.bodyPaper.safari();
                            break;
                        case 4:
                            self.slidersImgs[p].animateWith(sliderCanvas, anim, { transform: "T -" + self.itemDX * 2 + ", 0" }, self.animationDuration, ">");
                            self.slidersImgs[p].animateWith(sliderCanvas, anim, { opacity: "0" }, self.animationDuration, ">", self.updeteSlidersAfterPrev); // 
                            break;
                        default:
                    }

                }

            };
            self.updeteSlidersBeforePrev = function (sliderCanvas, anim) {
                var prevImage = {};
                var prevCanvas;
                var sliderFirst;
                if (self.lastDirection == directions[1]) {
                    prevImage = self.getNextSlideItem(self.Index - 1);
                } else {
                    prevImage = self.getNextSlideItem(self.Index - self.ImgCount);
                }

                sliderFirst = self.initFirst(prevImage);
                prevCanvas = self.initSliderImg(sliderFirst);
                prevCanvas.animateWith(sliderCanvas, anim, { opacity: " 0.6" }, self.animationDuration, ">");
                prevCanvas.animateWith(sliderCanvas, anim, { transform: "T " + self.itemDX + ", 0" }, self.animationDuration, ">");
                prevCanvas.toBack();
                self.bodyPaper.safari();
                self.slidersImgs.splice(0, 0, prevCanvas);

            };
            self.updeteSlidersAfterPrev = function () {
                var itemToRemove = {};
                //remove last slide
                for (var sliderImg in self.slidersImgs) {
                    if (self.slidersImgs[sliderImg].sliderItem.order == 4) {
                        itemToRemove = self.slidersImgs[sliderImg];
                    }
                }
                self.updeteAfterAnimationFinished(itemToRemove);

                //flag direction
                self.lastDirection = directions[1];
            };
            //endregion
            //#region Next
            var clickNextHandler = function () {
                var sliderCanvas = this.elenment;
                self.removeCorners();
                var animNext = Raphael.animation({ transform: "T -" + self.itemDX + ", " + self.itemDY }, self.animationDuration);
                self.updeteSlidersBeforeNext(sliderCanvas, animNext);
                for (var p = 0; p < self.slidersImgs.length; p++) {

                    switch (self.slidersImgs[p].sliderItem.order) {
                        case 0:
                            self.slidersImgs[p].animateWith(sliderCanvas, animNext, { transform: "T " + self.itemDX * 2 + ", 0" }, self.animationDuration, ">");
                            self.slidersImgs[p].animateWith(sliderCanvas, animNext, { opacity: " 0" }, self.animationDuration, ">");
                            break;
                        case 1:
                            self.slidersImgs[p].animateWith(sliderCanvas, animNext, { opacity: " 0.6" }, self.animationDuration, ">");
                            self.slidersImgs[p].animateWith(sliderCanvas, animNext, { transform: "T -" + self.itemDX + ", -" + self.itemDY }, self.animationDuration, ">");
                            self.bodyPaper.safari();
                            break;
                        case 2:
                            self.slidersImgs[p].animateWith(sliderCanvas, animNext, { transform: "T -" + self.itemDX + ", -" + self.itemDY }, self.animationDuration, ">");
                            self.bodyPaper.safari();
                            break;
                        case 3:
                            self.slidersImgs[p].animateWith(sliderCanvas, animNext, { transform: "T -" + self.itemDX + ", " + self.itemDY }, self.animationDuration, ">");
                            self.slidersImgs[p].toFront();
                            self.bodyPaper.safari();
                            break;
                        case 4:
                            self.slidersImgs[p].animateWith(sliderCanvas, animNext, { opacity: " 1" }, self.animationDuration, ">");
                            self.slidersImgs[p].animateWith(sliderCanvas, animNext, { transform: "T -" + self.itemDX + ", " + self.itemDY }, self.animationDuration, ">", self.updeteSlidersAfterNext);
                            self.bodyPaper.safari();
                            break;
                        default:
                    }

                }

            };
            self.updeteSlidersBeforeNext = function (sliderCanvas, anim) {
                var nextImage = {};
                var nextCanvas;
                var sliderFirst;
                if (self.lastDirection == directions[0]) {
                    nextImage = self.getNextSlideItem(self.Index + 1);
                } else {
                    nextImage = self.getNextSlideItem(self.Index + self.ImgCount);
                }

                sliderFirst = self.initLast(nextImage);
                nextCanvas = self.initSliderImg(sliderFirst);
                self.slidersImgs.splice(5, 0, nextCanvas);
                nextCanvas.animateWith(sliderCanvas, anim, { opacity: " 0.6" }, self.animationDuration, ">");
                nextCanvas.animateWith(sliderCanvas, anim, { transform: "T -" + self.itemDX + ", 0" }, self.animationDuration, ">");
                nextCanvas.toBack();

            };
            self.updeteSlidersAfterNext = function () {

                var itemToRemove = {};
                //remove last slide
                for (var sliderImg in self.slidersImgs) {
                    if (self.slidersImgs[sliderImg].sliderItem.order == 0) {
                        itemToRemove = self.slidersImgs[sliderImg];
                    }
                }
                self.updeteAfterAnimationFinished(itemToRemove);

                //flag direction
                self.lastDirection = directions[0];

            },
            //endregion

            //refreshes slideItem
            self.updeteAfterAnimationFinished = function (removeItem) {

                var buff = [];
                self.slidersImgs = jQuery.grep(self.slidersImgs, function (value) {
                    removeItem.remove();
                    return value != removeItem;
                });
                //create new canvas
                for (var p = 0; p < self.slidersImgs.length; p++) {
                    buff.push(self.initSliderImg(self.updateSequence(self.slidersImgs[p].sliderItem, p)));
                }
                //bind event
                for (var i = 0; i < buff.length; i++) {
                    self.initSliderClick(buff[i]);
                    if (buff[i].sliderItem.isCenter) {
                        buff[i].toFront();
                    }
                }

                //remove old
                self.cleanSlidesImg();
                //reassign from buffer
                self.slidersImgs = buff;
            },
            //endregion

            ///#region Slides creation
            self.initSliderImg = function (slider) {
                var group = self.bodyPaper.set();
                var sliderCanvas = {};
                if (slider.isCenter) {
                    sliderCanvas = self.initCenterSlide(slider);
                } else {

                    sliderCanvas = self.bodyPaper.image(slider.image, slider.layout.dx, slider.layout.dy, slider.layout.width, slider.layout.height);
                    //                     sliderCanvas = self.bodyPaper.rect(slider.layout.dx, slider.layout.dy, slider.layout.width, slider.layout.height);
                    //                    sliderCanvas.attr("stroke", "#CFD1D0");
                    //                    sliderCanvas.attr("fill", url(slider.image));
                    //                    sliderCanvas.attr("stroke-width", "2");
                    ////                    group.push(border);
                    if (slider.isDisebled) {
                        sliderCanvas.attr("opacity", 0.6);
                        sliderCanvas.toBack();
                    }
                }

                sliderCanvas.sliderItem = slider;
                self.initSliderCorner(slider);
                group.push(sliderCanvas);
                return sliderCanvas;
            };

            self.initCenterSlide = function (slider) {
                var layout = slider.layout;
                var group = self.bodyPaper.set();
                var groupShopsText = self.bodyPaper.set();
                var mouseX = 0;
                var mouseY = 0;
                //main image
                var sliderImg = self.bodyPaper.image(slider.image, layout.dx, layout.dy, layout.width, layout.height);

                ///centralShopsBar               
                //grey container for centralShopsBar :shops buttons and  name ,category, price
                var rect = self.bodyPaper.rect(layout.dx , layout.dy + (layout.height - 35), layout.width , 34);
                rect.attr({ fill: '#F1F1F1' });


                var rectHover = self.bodyPaper.rect(layout.dx-1, layout.dy, layout.width+2, layout.height+2);
                rectHover.attr("stroke", "#F1F1F1");
                rectHover.attr("stroke-width", "2");
                rectHover.toFront();
                group.push(rect);
                 group.push(rectHover);

                //shops buttons
                groupShopsText.push(self.initSlideShops(slider));

                //name ,category, price
                groupShopsText.push(self.initTextFields(slider));

                group.push(groupShopsText);
                group.push(sliderImg);

            

                sliderImg.mouseover(function () {
                    if (sliderImg.getBBox().height > (layout.height - self.centralShopsBarHeight)) {
                        sliderImg.animate({ "height": (layout.height - self.centralShopsBarHeight).toString() }, self.animationDuration, ">");
                    }
                });                                             rectHover.mouseout(function () {

                    if (sliderImg.getBBox().height < layout.height) {
                        sliderImg.animate({ "height": layout.height }, self.animationDuration, ">");
                    }
                });
                return group;
            },

            self.initSlideShops = function (slider) {
                var groupShops = self.bodyPaper.set();
                var layout = slider.layout;
                //name ,category, price
                for (var j = 0; j < slider.shops.length; j++) {
                    var shopBtn = self.bodyPaper.image(slider.shops[j].shopImgUrl, layout.dx + (layout.width - 60) + 15 * j, layout.dy + (layout.height - 35) + 20, 10, 10);
                    shopBtn.toFront();
                    shopBtn[0].onclick = function () {
                        var link = this.link;
                        window.location.href = link;
                    };
                    shopBtn[0].link = slider.shops[j].link;
                    groupShops.push(shopBtn);
                }
                return groupShops;
            },

            self.initTextFields = function (slider) {
                var layout = slider.layout;
                var groupText = self.bodyPaper.set();
                //name ,category, price
                var priceTxt = self.bodyPaper.text(layout.dx + (layout.width - slider.price.length * 5 + 2), layout.dy + (layout.height - 25), slider.price);
                priceTxt.attr({ "font-size": 13, "fill": "#89b254" });
                priceTxt.toFront();
                groupText.push(priceTxt);

                var categoryTxt = self.bodyPaper.text(layout.dx + slider.category.length * 5 + 2, layout.dy + layout.height - 10, slider.category);
                categoryTxt.attr({ "fill": "grey" });
                categoryTxt.toFront();
                groupText.push(categoryTxt);

                var nameTxt = self.bodyPaper.text(layout.dx + slider.name.length * 5 + 2, layout.dy + layout.height - 24, slider.name);
                nameTxt.attr({ "font-size": 16, "fill": "grey" });
                nameTxt.toFront();
                groupText.push(nameTxt);
                return groupText;
            },

            self.initSliderClick = function (sliderCanvas) {
                if (sliderCanvas.sliderItem.isPrev) {
                    sliderCanvas[0].elenment = sliderCanvas;
                    sliderCanvas[0].onclick = clickPrevHandler;
                }
                if (sliderCanvas.sliderItem.isNext) {
                    sliderCanvas[0].elenment = sliderCanvas;
                    sliderCanvas[0].onclick = clickNextHandler;
                }
            },
            //corners creation
            self.initSliderCorner = function (slider) {
                var layout = slider.layout;
                if (slider.hasCorner) {
                    var corner = self.bodyPaper.image(layout.CornerImgUrl, layout.dx + layout.CornerDx, layout.dy, self.itemCornerWidth, self.itemCornerHeight);
                    corner.attr("opacity", 0);
                    corner.animate({ opacity: 0.9 }, 250);
                    self.corners.push(corner);
                }
            },

            self.removeCorners = function () {
                for (var i = 0; i < self.corners.length; i++) {
                    self.corners[i].remove();
                }
            };
            //endregion
            //#region clean slidesImgs And slidsItems
            self.cleanSlidesImg = function () {
                for (var sliderImgItem in self.slidersImgs) {
                    self.slidersImgs[sliderImgItem].remove();
                }
                self.slidersImgs.length = 0;
            };

            //endregion

            return self;
        };

        //Generetes shop for slider source item  shops array
        FrontSlider.prototype.initShop = function (name, shopImgUrl, link) {
            var shop = {};
            shop.name = name;
            shop.shopImgUrl = shopImgUrl;
            shop.link = link;
            return shop;
        };

        //Generetes slider source item
        FrontSlider.prototype.sliderSourceItem = function (name, image, category, price, shops) {
            var sliderSourceItem = {};
            sliderSourceItem.name = name;
            sliderSourceItem.image = image;
            sliderSourceItem.shops = shops;
            sliderSourceItem.category = category;
            sliderSourceItem.price = price;
            return sliderSourceItem;
        };
        //  start initialization
        FrontSlider.prototype.init = function (sliderSources, domElement) {
            var mySlider = new FrontSlider();
            mySlider.init(sliderSources, domElement);
        };

        return FrontSlider;
    }

    var slider = new FrontSlider();
    var slidersImgSource = ["images/slider3.jpg", "images/slider1.jpg", "images/slider1.jpg", "images/slider3.jpg", "images/slider1.jpg"];
    // var slidersImgSource = ["images/first.png", "images/second.png", "images/third.png", "images/four.png", "images/five.png", "images/first.png", "images/second.png", "images/third.png", "images/four.png", "images/five.png"];
    var sliderSourceItems = [];

    //fill source items
    var sliderShops = [];
    var shopApple = slider.prototype.initShop('Apple', "images/Apple.jpg", "http://www.apple.com/");
    var shopAndroid = slider.prototype.initShop('Android', "images/Android.jpg", "http://www.android.com/");
    var shopBlackberry = slider.prototype.initShop('Blackberry', "images/Blackberry.jpg", "http://us.blackberry.com/");
    var shopWinPhone = slider.prototype.initShop('WinPhone', "images/WinPhone.jpg", "http://www.microsoft.com/windowsphone/uk-ua/default.aspx");
    sliderShops.push(shopApple);
    sliderShops.push(shopAndroid);
    sliderShops.push(shopBlackberry);
    sliderShops.push(shopWinPhone);

    sliderSourceItems.push(slider.prototype.sliderSourceItem('Slider3 First', slidersImgSource[0], "Games", "Free", sliderShops));
    sliderSourceItems.push(slider.prototype.sliderSourceItem('Slider1 Second', slidersImgSource[1], "Games", "Free", sliderShops));
    sliderSourceItems.push(slider.prototype.sliderSourceItem('Slider1 Third', slidersImgSource[2], "Gagets", "Free", sliderShops));
    sliderSourceItems.push(slider.prototype.sliderSourceItem('Slider3 Four', slidersImgSource[3], "Games", "Free", sliderShops));
    sliderSourceItems.push(slider.prototype.sliderSourceItem('Slider3 Five', slidersImgSource[4], "Games", "Free", sliderShops));



    ///initialize Slider
    slider.prototype.init(sliderSourceItems, "sliderBody");

});