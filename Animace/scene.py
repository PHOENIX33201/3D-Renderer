from manim import *
import math

def Sweeper(self, negative = -1):
    rect = Rectangle(height=10, width=20).move_to([16.5 * negative, 0, 0])
    
    rect.set_fill(BLACK, opacity=1)

    self.add(rect)
    return rect

def SweepLine(self, rect):
    l = Line((rect.get_x(), rect.get_y(), rect.get_z()), (-rect.get_x() + rect.get_width() * math.copysign(1, rect.get_x()), rect.get_y(), rect.get_z()))
    
    rect.set_z_index(1000)

    self.play(MoveAlongPath(rect, l), run_time=2)
    self.wait(0.2)

class CreateCircle(Scene):
    def construct(self):
        circle = Circle()  # create a circle
        circle.set_fill(PINK, opacity=0.5)  # set the color and transparency
        self.play(Create(circle))  # show the circle on screen

class SquareToCircle(Scene):
    def construct(self):
        circle = Circle()  # create a circle
        circle.set_fill(PINK, opacity=0.5)  # set color and transparency

        square = Square()  # create a square
        square.rotate(PI / 4)  # rotate a certain amount

        self.play(Create(square))  # animate the creation of the square
        self.play(Transform(square, circle))  # interpolate the square into the circle
        self.play(FadeOut(square))  # fade out animation

class TextTest(Scene):
    def construct(self):
        text1 = Tex(r"$\sqrt{a^2+\sqrt{b^2+c^2}^2} < distance$")
        text2 = Tex(r"$a^2+b^2+c^2 < distance^2$")

        self.play(Write(text1), run_time=5)
        self.wait(3)

class TextTest2(Scene):
    def construct(self):
        text1 = Tex(r"$\sqrt{a^2+\sqrt{b^2+c^2}^2} < distance$")
        text2 = Tex(r"$a^2+b^2+c^2 < distance^2$")

        self.add(text1)
        self.play(Transform(text1, text2), run_time=1.5)  # interpolate the square into the circle
        self.wait(3)

class TextTest3(Scene):
    def construct(self):
        circle = Ellipse(color = BLUE, width = 15, height = 2.5)  # create a circle
        circle.set_fill(BLUE, opacity=1)  # set the color and transparency
        
        text1 = Tex(r"Štít je zapnutý nebo zničený", height = 1.15)
        text1.z_index=1

        self.play(Write(text1), run_time=4)
        self.wait(0.1)
        
        self.play(Create(circle), run_time=1)  # show the circle on screen
        self.wait(1)

class WriteText1(Scene):
    def construct(self):
        rect = Sweeper(self)

        text1 = Tex(r"3D Renderer", width=150)

        self.play(Write(text1), run_time=3)
        
        self.wait(0.2)

class WriteText1E(Scene):
    def construct(self):
        rect = Sweeper(self)

        text1 = Tex(r"3D Renderer", width=150)

        self.add(text1)
        
        SweepLine(self, rect)

class WriteText2(Scene):
    def construct(self):
        rect = Sweeper(self, 1)

        text1 = Tex(r"Jak zapsat 3D objekt v programu?", width=150).move_to([-2, 3, 0])
        text2 = Tex(r"Vrcholy: $[(x_{1}, y_{1}, z_{1}), (x_{2}, y_{2}, z_{2})...]$", width=120).move_to([-2.75, 2.5, 0])
        text2_2 = Tex(r"Vrcholy: $[v_{1}, v_{2}...]$", width=60).move_to([-4.25, 2.5, 0])
        text3 = Tex(r"Hrany: $[(a_{1}, b_{1}), (a_{2}, b_{2})...]$", width=90).move_to([-3.5, 2, 0])

        d = VGroup()
        d.add(Dot((0, 0, 0)))
        d.add(Dot((2, 0, 0)))
        d.add(Dot((2, 2, 0)))
        d.add(Dot((0, 2, 0)))

        d.add(Dot((0.84, 0.84, 2)))
        d.add(Dot((2.84, 0.84, 2)))
        d.add(Dot((2.84, 2.84, 2)))
        d.add(Dot((0.84, 2.84, 2)))

        d.move_to([1, -1, 0])

        self.play(Write(text1), run_time=3)
        self.wait(0.2)
        self.play(Write(text2), run_time=2.5)
        self.wait(0.2)
        self.play(Transform(text2, text2_2), run_time=1.5)
        self.wait(0.2)

        for obj in d:
            self.add(obj)
            self.wait(0.2)

        self.play(Write(text3), run_time=2)

        l = [
        (0, 1), 
        (1, 2), 
        (2, 3), 
        (3, 0), 

        (1, 5), 
        (2, 6), 
        (3, 7), 

        (5, 6), 
        (6, 7), 
        ]

        for p in l:
            a,b = p
            self.play(Create(Line(d[a], d[b])), run_time=0.3)

        l = [
        (4, 0), 
        (4, 5), 
        (4, 7), 
        ]

        for p in l:
            a,b = p
            self.play(Create(DashedLine(d[a], d[b], dash_length=0.1, dashed_ratio=0.6)), run_time=0.3)

        self.wait(0.2)

class WriteText2E(Scene):
    def construct(self):
        rect = Sweeper(self, 1)

        text1 = Tex(r"Jak zapsat 3D objekt v programu?", width=150).move_to([-2, 3, 0])
        text2_2 = Tex(r"Vrcholy: $[v_{1}, v_{2}...]$", width=60).move_to([-4.25, 2.5, 0])
        text3 = Tex(r"Hrany: $[(a_{1}, b_{1}), (a_{2}, b_{2})...]$", width=90).move_to([-3.5, 2, 0])

        d = VGroup()
        d.add(Dot((0, 0, 0)))
        d.add(Dot((2, 0, 0)))
        d.add(Dot((2, 2, 0)))
        d.add(Dot((0, 2, 0)))

        d.add(Dot((0.84, 0.84, 2)))
        d.add(Dot((2.84, 0.84, 2)))
        d.add(Dot((2.84, 2.84, 2)))
        d.add(Dot((0.84, 2.84, 2)))

        d.move_to([1, -1, 0])

        self.add(text1)
        self.add(text2_2)

        for obj in d:
            self.add(obj)

        self.add(text3)

        l = [
        (0, 1), 
        (1, 2), 
        (2, 3), 
        (3, 0), 

        (1, 5), 
        (2, 6), 
        (3, 7), 

        (5, 6), 
        (6, 7), 
        ]

        for p in l:
            a,b = p
            self.add(Line(d[a], d[b]))

        l = [
        (4, 0), 
        (4, 5), 
        (4, 7), 
        ]

        for p in l:
            a,b = p
            self.add(DashedLine(d[a], d[b], dash_length=0.1, dashed_ratio=0.6))

        SweepLine(self, rect)

class WriteText3(Scene):
    def construct(self):
        rect = Sweeper(self)

        text1 = Tex(r"Jak zobrazit 3D objekt na 2D displeji?", width=150).move_to([-2, 3, 0])
        text1_2 = Tex(r"Projekce", width=30).move_to([-5, 3, 0])
        text2 = MathTex(r"x_{projected} =", width=40).move_to([-4.75, 2, 0])
        text2[0][:10].set_fill(color=RED)
        text2_2 = MathTex(r"\text{x * focal length}", r"\over", r"\text{z + focal length}", width=40).move_to([-2.58, 2, 0])
        text2_2[0][0].set_fill(color=BLUE)
        text2_2[0][2:].set_fill(color=YELLOW)
        text2_2[2][2:].set_fill(color=YELLOW)
        text2_2[2][0].set_fill(color=GREEN)
        text2_3 = MathTex(r"\text{+}", width=3).move_to([-1.45, 2, 0])
        text2_4 = MathTex(r"\text{šířka displeje}", r"\over", r"2", width=37).move_to([-0.4, 2.025, 0])

        text3 = MathTex(r"y_{projected} =", width=40).move_to([-4.75, 1, 0])
        text3_2 = MathTex(r"\text{y * focal length}", r"\over", r"\text{z + focal length}", width=40).move_to([-2.58, 1, 0])
        text3_3 = MathTex(r"\text{+}", width=3).move_to([-1.45, 1, 0])
        text3_4 = MathTex(r"\text{výška displeje}", r"\over", r"2", width=37).move_to([-0.4, 1.025, 0])

        text4 = Tex(r"focal length", width=30, color=YELLOW).move_to([3.6, -1.5, 0])
        text5 = Tex(r"z", width=3, color=GREEN).move_to([2.9, 0, 0])
        text6 = Tex(r"x", width=3, color=BLUE).move_to([2.2, 0.7, 0])
        text7 = Tex(r"$x_{projected}$", width=20, color=RED).move_to([1.9, -0.3, 0])

        self.play(Write(text1), run_time=3)
        self.wait(0.2)
        self.play(Transform(text1, text1_2), run_time=1.5)
        self.wait(0.2)

        d1 = Dot((2.7, -2.5, 0), color=YELLOW)
        d2 = Dot((1.7, 0.5, 0), color=BLUE)
        d3 = Dot((2, -0.5, 0), color=RED)
        d4 = Dot((d1.get_x(), -0.5, 0))

        line = Line((0.7, -0.5, 0), (4.7, -0.5, 0))
        self.play(Create(line), run_time=0.3)
        self.add(d4)
        self.wait(0.3)
        self.add(d1)
        self.wait(0.3)
        self.add(d2)
        self.wait(0.3)
        

        self.play(Create(DashedLine(d1, d2, dash_length=0.2, dashed_ratio=0.4)), run_time=0.3)
        self.add(d3)
        self.wait(0.2)

        self.play(Create(Line((d1.get_x(), -0.5, 0), (2, -0.5, 0), color=RED)), run_time=0.3)
        self.play(Write(text7), Write(text2), Write(text2_2[1]), run_time=1.5)
        self.wait(0.2)

        self.play(Create(Line(d1, (d1.get_x(), -0.5, 0), color=YELLOW)), run_time=0.5)
        self.play(Write(text4), Write(text2_2[0][1:]), Write(text2_2[2][1:]), run_time=2)
        self.wait(0.2)

        self.play(Create(Line((d1.get_x(), -0.5, 0), (d1.get_x(), d2.get_y(), 0), color=GREEN)), run_time=0.3)
        self.play(Write(text5), Write(text2_2[2][0]), run_time=0.3)

        self.play(Create(Line((d1.get_x(), d2.get_y(), 0), d2, color=BLUE)), run_time=0.3)
        self.play(Write(text6), Write(text2_2[0][0]), run_time=0.3)
        self.wait(0.2)

        self.play(Write(text3), Write(text3_2), run_time=2)
        self.wait(0.2)

        self.play(Write(text2_3), Write(text2_4), Write(text3_3), Write(text3_4), run_time=2)

        self.wait(0.2)

class WriteText3E(Scene):
    def construct(self):
        rect = Sweeper(self)

        text1_2 = Tex(r"Projekce", width=30).move_to([-5, 3, 0])
        text2 = MathTex(r"x_{projected} =", width=40).move_to([-4.75, 2, 0])
        text2[0][:10].set_fill(color=RED)
        text2_2 = MathTex(r"\text{x * focal length}", r"\over", r"\text{z + focal length}", width=40).move_to([-2.58, 2, 0])
        text2_2[0][0].set_fill(color=BLUE)
        text2_2[0][2:].set_fill(color=YELLOW)
        text2_2[2][2:].set_fill(color=YELLOW)
        text2_2[2][0].set_fill(color=GREEN)
        text2_3 = MathTex(r"\text{+}", width=3).move_to([-1.45, 2, 0])
        text2_4 = MathTex(r"\text{šířka displeje}", r"\over", r"2", width=37).move_to([-0.4, 2.025, 0])

        text3 = MathTex(r"y_{projected} =", width=40).move_to([-4.75, 1, 0])
        text3_2 = MathTex(r"\text{y * focal length}", r"\over", r"\text{z + focal length}", width=40).move_to([-2.58, 1, 0])
        text3_3 = MathTex(r"\text{+}", width=3).move_to([-1.45, 1, 0])
        text3_4 = MathTex(r"\text{výška displeje}", r"\over", r"2", width=37).move_to([-0.4, 1.025, 0])

        text4 = Tex(r"focal length", width=30, color=YELLOW).move_to([3.6, -1.5, 0])
        text5 = Tex(r"z", width=3, color=GREEN).move_to([2.9, 0, 0])
        text6 = Tex(r"x", width=3, color=BLUE).move_to([2.2, 0.7, 0])
        text7 = Tex(r"$x_{projected}$", width=20, color=RED).move_to([1.9, -0.3, 0])

        self.add(text1_2)

        d1 = Dot((2.7, -2.5, 0), color=YELLOW)
        d2 = Dot((1.7, 0.5, 0), color=BLUE)
        d3 = Dot((2, -0.5, 0), color=RED)
        d4 = Dot((d1.get_x(), -0.5, 0))

        line = Line((0.7, -0.5, 0), (4.7, -0.5, 0))
        self.add(line)
        self.add(d4)
        self.add(d1)
        self.add(d2)
        
        self.add(text2, text2_2, text2_3, text2_4)
        self.add(text3, text3_2, text3_3, text3_4)
        self.add(text4, text5, text6, text7)

        self.add(DashedLine(d1, d2, dash_length=0.2, dashed_ratio=0.4))
        self.add(d3)
        self.add(Line((d1.get_x(), -0.5, 0), (2, -0.5, 0), color=RED))
        self.add(Line(d1, (d1.get_x(), -0.5, 0), color=YELLOW))
        self.add(Line((d1.get_x(), -0.5, 0), (d1.get_x(), d2.get_y(), 0), color=GREEN))
        self.add(Line((d1.get_x(), d2.get_y(), 0), d2, color=BLUE))

        text1_2.set_z_index(1001)

        SweepLine(self, rect)

class WriteText4(Scene):
    def construct(self):
        rect = Sweeper(self, 1)

        text1 = Tex(r"Projekce", width=30).move_to([-5, 3, 0])
        text2 = Tex(r"vrcholy", width=21).move_to([-5.2, 2.5, 0])
        text3 = Tex(r"hrany", width=15).move_to([-3.1, 2.5, 0])

        self.add(text1)

        self.play(Write(text2), Write(text3), run_time=0.6)

        vertices = VGroup()
        vertices.add(Dot((-2, -2, 0)))
        vertices.add(Dot((-2,  2, 0)))
        vertices.add(Dot(( 2,  2, 0)))
        vertices.add(Dot(( 2, -2, 0)))

        vertices.add(Dot((-1.5, -1.5, 0)))
        vertices.add(Dot((-1.5,  1.5, 0)))
        vertices.add(Dot(( 1.5,  1.5, 0)))
        vertices.add(Dot(( 1.5, -1.5, 0)))

        pos = 0
        writeList = []

        for v in vertices:
            x, y = v.get_x(), v.get_y()
            if x < 0:
                t1 = f"(-{abs(x):>{1}.{1}f}, "
            else:
                t1 = f"( {abs(x):>{1}.{1}f}, "

            if y < 0:
                t2 = f"-{abs(y):>{1}.{1}f})"
            else:
                t2 = r"\hspace{0.1cm}" + f"{abs(y):>{1}.{1}f})"

            t = Tex(t1, t2, width=20).move_to([-5.2, 2.2 - pos, 0])
            
            writeList.append(Write(t))
            pos += 0.3

        vertices.move_to([1.5, -1, 0])

        self.play(*writeList, Create(vertices), run_time=0.7)

        self.wait(0.2)

        connections = [
            (0, 1),
            (1, 2),
            (2, 3),
            (3, 0),

            (4, 5),
            (5, 6),
            (6, 7),
            (7, 4),

            (0, 4),
            (1, 5),
            (2, 6),
            (3, 7),
        ]

        pos = 0
        writeList = []

        for c in connections:
            a, b = c

            t = Tex(f"({a}, {b})", width=10).move_to([-3.2, 2.2 - pos, 0])

            writeList.append(Write(t))
            pos += 0.3

        self.play(*writeList, run_time=0.7)

        self.wait(0.2)

        lines = []

        i = 0
        dio = True
        for c in connections:
            a, b = c

            a1 = Arrow(max_tip_length_to_length_ratio = 0.1, max_stroke_width_to_length_ratio=1)
            a1.put_start_and_end_on((-3.5, 2.2 - i * 0.3, 0), (-4.6, 2.2 - a * 0.3, 0))
            a2 = Arrow(max_tip_length_to_length_ratio = 0.1, max_stroke_width_to_length_ratio=1)
            a2.put_start_and_end_on((-3.5, 2.2 - i * 0.3, 0), (-4.6, 2.2 - b * 0.3, 0))

            l = Line(vertices[a], vertices[b])
            lines.append(l)

            self.play(Create(a1), Create(a2), Create(l), run_time=0.5)

            if dio: self.wait(0.2); dio = False

            self.play(Uncreate(a1.get_tip()), Uncreate(a2.get_tip()), run_time=0.1)
            self.play(Uncreate(a1), Uncreate(a2), run_time=0.3)

            i += 1
        

        self.wait(0.2)

        for v in vertices:
            self.remove(v)

        self.wait(0.2)

        transformList = []

        for i in range(len(lines)):
            a, b = connections[i]
            line = lines[i]
            transformList.append(Transform(line, Line((vertices[a].get_x() * 1.05, vertices[a].get_y() * 1.05, 0), (vertices[b].get_x() * 1.05, vertices[b].get_y() * 1.05, 0))))
        self.play(*transformList, run_time=0.2)  
        

        self.wait(0.2)

class WriteText4E(Scene):
    def construct(self):
        rect = Sweeper(self, 1)

        text1 = Tex(r"Projekce", width=30).move_to([-5, 3, 0])
        text2 = Tex(r"body", width=12).move_to([-5.45, 2.5, 0])
        text3 = Tex(r"konekse", width=21).move_to([-3, 2.5, 0])

        self.add(text1)
        self.add(text2, text3)

        vertices = VGroup()
        vertices.add(Dot((-2, -2, 0)))
        vertices.add(Dot((-2,  2, 0)))
        vertices.add(Dot(( 2,  2, 0)))
        vertices.add(Dot(( 2, -2, 0)))

        vertices.add(Dot((-1.5, -1.5, 0)))
        vertices.add(Dot((-1.5,  1.5, 0)))
        vertices.add(Dot(( 1.5,  1.5, 0)))
        vertices.add(Dot(( 1.5, -1.5, 0)))

        pos = 0
        writeList = []

        for v in vertices:
            x, y = v.get_x(), v.get_y()
            if x < 0:
                t1 = f"(-{abs(x):>{1}.{1}f}, "
            else:
                t1 = f"( {abs(x):>{1}.{1}f}, "

            if y < 0:
                t2 = f"-{abs(y):>{1}.{1}f})"
            else:
                t2 = r"\hspace{0.1cm}" + f"{abs(y):>{1}.{1}f})"

            t = Tex(t1, t2, width=20).move_to([-5.2, 2.2 - pos, 0])
            
            writeList.append(t)
            pos += 0.3

        vertices.move_to([1.5, -1, 0])

        self.add(*writeList)

        connections = [
            (0, 1),
            (1, 2),
            (2, 3),
            (3, 0),

            (4, 5),
            (5, 6),
            (6, 7),
            (7, 4),

            (0, 4),
            (1, 5),
            (2, 6),
            (3, 7),
        ]

        pos = 0
        writeList = []

        for c in connections:
            a, b = c

            t = Tex(f"({a}, {b})", width=10).move_to([-3.2, 2.2 - pos, 0])

            writeList.append(t)
            pos += 0.3

        self.add(*writeList)

        transformList = []

        for i in range(len(connections)):
            a, b = connections[i]
            transformList.append(Line((vertices[a].get_x() * 1.05, vertices[a].get_y() * 1.05, 0), (vertices[b].get_x() * 1.05, vertices[b].get_y() * 1.05, 0)))

        self.add(*transformList)  
        

        SweepLine(self, rect)

class WriteText5(Scene):
    def construct(self):
        rect = Sweeper(self)

        text1 = Tex(r"Rotace", width=22.5).move_to([-5, 3, 0])
        text2 = MathTex(r"q = a+bi+cj+dk", r"i^{2}=j^{2}=k^{2} = -1",  r"ij = -ji = k", r"jk = -kj = i", r"ki = -ik = j", r"ijk = -1", width=250)
        text3 = Tex(r"eater.net/quaternions", width=100).move_to([2, 0, 0])
        

        self.play(Write(text1), run_time=0.7)

        stacked_equations = VGroup(*text2).move_to([-9, 3, 0])
        for i, equation in enumerate(stacked_equations):
            equation.next_to(stacked_equations[i - 1], DOWN, buff=0.3)

        self.play(Write(stacked_equations), run_time=2)
        
        self.wait(0.2)

        self.play(Write(text3), run_time=1.5)

        self.wait(0.2)

class WriteText5E(Scene):
    def construct(self):
        rect = Sweeper(self)

        text1 = Tex(r"Rotace", width=22.5).move_to([-5, 3, 0])
        text2 = MathTex(r"q = a+bi+cj+dk", r"i^{2}=j^{2}=k^{2} = -1",  r"ij = -ji = k", r"jk = -kj = i", r"ki = -ik = j", r"ijk = -1", width=250)
        text3 = Tex(r"eater.net/quaternions", width=100).move_to([2, 0, 0])
        
        self.add(text1)

        stacked_equations = VGroup(*text2).move_to([-9, 3, 0])
        for i, equation in enumerate(stacked_equations):
            equation.next_to(stacked_equations[i - 1], DOWN, buff=0.3)

        self.add(stacked_equations)
        self.add(text3)

        SweepLine(self, rect)

class WriteText6(Scene):
    def construct(self):
        rect = Sweeper(self, 1)

        text1 = Tex(r"Parenting system", width=60).move_to([-4, 3, 0])
        text2 = Tex(r"...", width=15).move_to([-0.5 , -2, 0])

        self.play(Write(text1), run_time=2)
        self.wait(0.2)


        r1 = Rectangle(height=1, width=1, color=GREEN).move_to([0 , -0.96, 0])
        r1.set_fill(GREEN, opacity=1)
        r1.generate_target()
        r1.set_z_index(3)
        r2 = Rectangle(height=1, width=2, color=BLUE).move_to([0 , -2, 0])
        r2.set_fill(BLUE, opacity=1)
        r2.generate_target()
        r2.set_z_index(2)
        r3 = Rectangle(height=1, width=3, color=RED).move_to([0 , -2.04, 0])
        r3.set_fill(RED, opacity=1)
        r3.generate_target()
        r3.set_z_index(1)

        self.play(Create(r1), Create(r2), run_time=1)
        self.wait(0.2)
        
        r1.target.shift(0.5*RIGHT)
        self.play(MoveToTarget(r1), run_time=0.5)
        self.wait(0.2)

        r2.target.shift(RIGHT)
        self.play(MoveToTarget(r2), run_time=0.5)
        self.wait(0.2)

        r1.target.shift(LEFT)
        r2.target.shift(LEFT)
        self.play(MoveToTarget(r1), MoveToTarget(r2), run_time=0.5)
        self.wait(0.2)

        r1.target.shift(UP)
        r2.target.shift(UP)
        self.play(MoveToTarget(r1), MoveToTarget(r2), run_time=0.5)
        self.play(Create(r3), run_time=0.5) 
        self.wait(0.2)

        r1.target.shift(UP)
        r2.target.shift(UP)
        r3.target.shift(UP)
        self.play(MoveToTarget(r3), run_time=0.5)
        self.play(MoveToTarget(r2), run_time=0.5)
        self.play(MoveToTarget(r1), run_time=0.5)
        
        self.wait(0.2)

        self.play(Write(text2), run_time=0.5) 


        self.wait(0.2)

class WriteText6E(Scene):
    def construct(self):
        rect = Sweeper(self, 1)

        text1 = Tex(r"Parenting system", width=60).move_to([-4, 3, 0])
        text2 = Tex(r"...", width=15).move_to([-0.5 , -2, 0])

        self.add(text1)


        r1 = Rectangle(height=1, width=1, color=GREEN).move_to([-0.5 , 1.04, 0])
        r1.set_fill(GREEN, opacity=1)
        r2 = Rectangle(height=1, width=2, color=BLUE).move_to([0 , 0, 0])
        r2.set_fill(BLUE, opacity=1)
        r3 = Rectangle(height=1, width=3, color=RED).move_to([0 , -1.04, 0])
        r3.set_fill(RED, opacity=1)


        self.add(r3)
        self.add(r2)
        self.add(r1)

        self.add(text2) 


        SweepLine(self, rect)

class WriteText7(Scene):
    def construct(self):
        rect = Sweeper(self)

        text1 = Tex(r"Děkuji za pozornost", width=150).move_to([0 , 3, 0])

        self.play(Write(text1), run_time=3)
        
        self.wait(0.2)

# python -m manim scene.py SquareToCircle -p