# WeSP-Editor
A simple program for creating and editing SVG Paths for the web. It is a bit like a pixel art editor but for SVGs, you draw the individual points in the path and have total control over their properties.

![WeSP](WeSP.jpg?raw=true "WeSP")

I started this project for fun, to learn about SVGs and to make something easy to use for making/editing SVG paths for the web.

For a binary of this project check https://github.com/Marius4th/WeSP-Editor/releases

You can contact me at [@marius4th](https://twitter.com/marius4th) on twitter.

Check 'Help > View Help' inside the program for a list of Key Bindings.

### Some things on my To Do list:

- [ ] Implement all Commands:
  - [x] Movetos.
  - [x] Linetos.
     - You can use SHIFT for right angles, which will convert linetos into their horizontal/vertical equivalent in html code.
  - [x] Quadratic Bezier.
  - [x] Curvetos (Cubic Beziers).
  - [x] Implement loading of smooth versions of QBezier and Curveto.
     - Implement smooth versions of QBezier and Curveto as Drawing Tools?
  - [x] Fully implement Elliptical Arcs.

- [x] Implement simple undo and redo.
- [ ] Implement better, more effecient, undo and redo.
- [x] Improve mirroring points.
- [ ] Improve mirroring points even more ^^.
- [x] Add path string optimizations to compress/shorten it as much as possible.
- [ ] Improve the interface.
- [x] Implement paths and figures (subpaths) reordering.
- [ ] Points locking so you can't modify them accidentally?
- [x] Background template images.
- [ ] Custom crusors for when pressing mod keys like CTRL, SHILT or ALT to easily see what tool/action you have selected.
- [x] Implement proper scaling (the actual solution has issues because of floating point errors).
  -Fixed by using DrwingPath's Transform matrix for the preview and then modifying the point's position for real when hitting the 'Apply' button.
- [ ] Add settings to set the drawing quality so you can lower it to improve performance.
- [x] Drag & Drop for loading template images and wsvg files.
- [ ] Button to show/hide figures and paths.
- [ ] Button to duplicate figures and paths.
- [ ] Change "Path props" to a list of path's attributes where you can add remove them (maybe add a list for every element in the file).
- [ ] Support other elements beside paths, like circles, ellipses, rects, texts...

Feedback and Sugestions are welcome.
